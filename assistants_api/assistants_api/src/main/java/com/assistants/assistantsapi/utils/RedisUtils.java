package com.assistants.assistantsapi.utils;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Component;

import java.util.concurrent.TimeUnit;

/**
 * @author Harris
 * @since 2024-05-23 11:08
 */
@Component
public class RedisUtils {
    private final RedisTemplate<String, Object> redisTemplate;

    @Autowired
    public RedisUtils(RedisTemplate<String, Object> redisTemplate) {
        this.redisTemplate = redisTemplate;
    }

    public void setStrExp(String key, String val) {
        redisTemplate.opsForValue().set(key, val, 5 * 60 * 1000, TimeUnit.MILLISECONDS);
    }

    public void setStrExp(String key, String val, long time) {
        redisTemplate.opsForValue().set(key, val, time, TimeUnit.MILLISECONDS);
    }

    public String getStr(String key) {
        return (String) redisTemplate.opsForValue().get(key);
    }
}
