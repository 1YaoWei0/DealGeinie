package com.assistants.assistantsapi.job;

import cn.hutool.http.HttpRequest;
import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONArray;
import com.assistants.assistantsapi.config.env.AzureAdEnvConfig;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;


/**
 * @author Harris
 * @since 2024-05-14 17:27
 */
@Slf4j
@Component
public class UserInfoSynUtils {

    private final AzureAdEnvConfig adConfig;

    @Autowired
    public UserInfoSynUtils(AzureAdEnvConfig adConfig) {
        this.adConfig = adConfig;
    }

    /**
     * 获取access_token
     */
    public String getAccessToken() {
        String formData = "grant_type=client_credentials&client_id=" + adConfig.getClientId() + "&scope=" + adConfig.getScope() + "&client_secret=" + adConfig.getClientSecret() + "&resource=" + adConfig.getResource();
        String result = HttpRequest.post(adConfig.getTokenUserUrl())
                .header("Content-Type", "application/x-www-form-urlencoded;charset=utf-8")
                .body(formData)
                .execute().body();

        String accessToken = JSON.parseObject(result).getString("access_token");
        if (StringUtils.isBlank(accessToken)) {
            log.error("用户信息同步，令牌获取失败");
            return null;
        }
        return accessToken;
    }

    /**
     * 获取所有用户信息,并同步
     *
     * @throws IOException 网络请求抛出IO异常
     */
    public void getAllUserInfo() throws IOException {
        String accessToken = getAccessToken();
        log.info("获取令牌: {}", accessToken);
        if (accessToken != null) {
            //响应数据过大，通过文件流的方式读取
            InputStream authorization = HttpRequest.get(adConfig.getAllUserinfoUrl()).header("Authorization", accessToken).execute().bodyStream();
            BufferedReader br = new BufferedReader(new InputStreamReader(authorization));
            StringBuilder stringBuffer = new StringBuilder();
            String str;
            while ((str = br.readLine()) != null) {
                stringBuffer.append(str);
            }
            //解析json
            JSONArray userLists = JSON.parseObject(stringBuffer.toString()).getJSONArray("value");
            log.info("获取用户信息: {}", userLists);
        }
    }
}
