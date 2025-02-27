package com.assistants.assistantsapi.job.handler;

import com.assistants.assistantsapi.job.UserInfoSynUtils;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.time.LocalDateTime;

/**
 * @author Harris
 * @since 2024-07-05 14:05
 */
@Slf4j
@Component
public class MyJobHandler {

    private final UserInfoSynUtils userInfoSynUtils;

    @Autowired
    public MyJobHandler(UserInfoSynUtils userInfoSynUtils) {
        this.userInfoSynUtils = userInfoSynUtils;
    }
    public void syncAdUserInfoJob() {
        try {
            log.info("定时任务开始执行: " + LocalDateTime.now());
            userInfoSynUtils.getAllUserInfo();
            log.info("定时任务结束执行: " + LocalDateTime.now());
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
