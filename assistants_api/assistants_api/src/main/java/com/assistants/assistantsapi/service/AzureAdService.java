package com.assistants.assistantsapi.service;

import cn.hutool.http.HttpRequest;
import cn.hutool.http.HttpUtil;
import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson2.JSONObject;
import com.assistants.assistantsapi.config.env.AzureAdEnvConfig;
import com.assistants.assistantsapi.utils.RsaUtils;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

/**
 * @author Harris
 * @since 2024-05-14 16:15
 */
@Slf4j
@Service
public class AzureAdService {

    private final AzureAdEnvConfig adConfig;

    @Autowired
    public AzureAdService(AzureAdEnvConfig adConfig) {
        this.adConfig = adConfig;
    }

    public String getAuthorizeUrl(String host) {
        String redirectUri = StringUtils.isBlank(host) ? adConfig.getRedirectUri() : "https://" + host + "/login/oauth";
        return adConfig.getAuthorizeUrl() + "?client_id=" + adConfig.getClientId() +
                "&response_type=code&scope=" + adConfig.getScope() + "&state=" + UUID.randomUUID() +
                "&redirect_uri=" + redirectUri;
    }

    public String loginOauth(String code) {
        String formData = "grant_type=authorization_code&client_id=" + adConfig.getClientId() + "&code=" + code + "&client_secret=" + adConfig.getClientSecret() + "&redirect_uri=" + adConfig.getRedirectUri();
        String result = HttpRequest.post(adConfig.getTokenUrl())
                .header("Content-Type", "application/x-www-form-urlencoded")
                .body(formData)
                .execute().body();

        JSONObject resultObj = com.alibaba.fastjson2.JSON.parseObject(result);
        String accessToken = resultObj.getString("access_token");
        if (StringUtils.isBlank(accessToken)) {
            log.error("请求不合法，令牌为空");
        }
        log.info("accessToken：" + accessToken);
        String openIdConGetResult = HttpUtil.get(adConfig.getOpenidConnectUrl());
        String jwks_uri = JSON.parseObject(openIdConGetResult).getString("jwks_uri");
        String userName = RsaUtils.tokenRsaVerifyMethod(accessToken, jwks_uri);
        String[] splits = userName.split("\"");
        log.info("当前登录用户：" + splits[1]);
        return splits[1].toLowerCase();
    }
}
