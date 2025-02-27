package com.assistants.assistantsapi.interceptor;

import com.alibaba.fastjson.JSON;
import com.assistants.assistantsapi.annota.AccessLimit;
import com.assistants.assistantsapi.exceptions.AccessDeniedException;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Component;
import org.springframework.web.method.HandlerMethod;
import org.springframework.web.servlet.HandlerInterceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Enumeration;
import java.util.concurrent.TimeUnit;

/**
 * @author Harris
 * @since 2024-05-24 17:19
 */
@Slf4j
@Component
public class AccessLimtInterceptor implements HandlerInterceptor {
    @Autowired
    private RedisTemplate<String, Object> redisTemplate;

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        if (handler instanceof HandlerMethod) {
            HandlerMethod handlerMethod = (HandlerMethod) handler;
            AccessLimit accessLimit = handlerMethod.getMethodAnnotation(AccessLimit.class);
            if (accessLimit == null) {
                return true;
            }
            int seconds = accessLimit.seconds();
            int maxCount = accessLimit.maxCount();
            boolean needLogin = accessLimit.needLogin();
            if (needLogin) {
                //TODO 登录判断，目前不需要，默认false
            }
            String remoteAddr = request.getRemoteAddr();
            log.info("remoteAddr:{}",remoteAddr);
            Enumeration<String> headerNames = request.getHeaderNames();
            while (headerNames.hasMoreElements()) {
                String name = headerNames.nextElement();
                String value = request.getHeader(name);
                log.info("name:{},value:{}",name,value);
            }
            if(StringUtils.isNotBlank(remoteAddr)){
                return true;
            }
            String key = request.getServletPath() + ":" + remoteAddr;
            Integer count = (Integer) redisTemplate.opsForValue().get(key);
            if (null == count || -1 == count) {
                redisTemplate.opsForValue().set(key, 1, seconds, TimeUnit.SECONDS);
                return true;
            }

            if (count < maxCount) {
                count = count + 1;
                redisTemplate.opsForValue().set(key, count, 0);
                return true;
            }

            // response 返回 json 请求过于频繁请稍后再试
            throw new AccessDeniedException(20001, "操作过于频繁,请稍后再试");

        }
        return true;
    }
}
