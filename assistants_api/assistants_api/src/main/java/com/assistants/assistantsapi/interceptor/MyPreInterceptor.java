package com.assistants.assistantsapi.interceptor;

import com.alibaba.fastjson.JSONObject;
import com.assistants.assistantsapi.utils.Sha256Utils;
import org.apache.commons.lang3.StringUtils;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.HandlerInterceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.PrintWriter;

/**
 * @author Harris
 * @since 2024-05-24 10:28
 */
@Component
public class MyPreInterceptor implements HandlerInterceptor {
    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        String header = request.getHeader("Token-Key");
        String key = "MsxLz9wJM4WQVp+wU+RMNwiusl944zhy26Q56ue+whUpDO19Jp1fhrj73TquZo3w6iJFzGgct5jbiYQdXuMaupldX7NvFS4EUkKCwDMPWG31RXQ7mgnbuvYIMpl5LjQGZYQk4niN0wRAgMBAAECggEAG/4qj6r6fu4JwFR9GWk2uXObyh9fok30ZHEjMDYpnBhwbXvac8MnNKqI1Me5mf4EcKsvP7XtBjEbTRVHGMconm5zse5iIgUnLO67Ingp9vXGUDroB6hIRy";
        String defaultKey = Sha256Utils.sha256Digester(key);
        if (StringUtils.equals(header, defaultKey)){
            return true;
        }else{
            response.setContentType("application/json;charset=utf-8");
            response.setStatus(401);
            PrintWriter writer = response.getWriter();
            JSONObject object = new JSONObject();
            object.put("code",401);
            object.put("message","未经授权");
            writer.println(object.toJSONString());
            writer.flush();
            writer.close();
            return false;
        }
    }
}
