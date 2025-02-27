package com.assistants.assistantsapi.controller;

import com.assistants.assistantsapi.annota.AccessLimit;
import com.assistants.assistantsapi.service.AzureAdService;
import com.assistants.assistantsapi.utils.ResultUtils;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

/**
 * @author Harris
 * @since 2024-05-14 16:14
 */
@Slf4j
@RestController
@RequestMapping("/api/ad")
public class AzureAdController {

    private final AzureAdService adService;

    public AzureAdController(AzureAdService adService) {
        this.adService = adService;
    }
    @AccessLimit(seconds = 60, maxCount = 5)
    @GetMapping("/authorize")
    public ResultUtils getAuthorizeUrl() {
        log.info("请求授权");
        String authorizeUrl = adService.getAuthorizeUrl(null);
        return ResultUtils.Ok(null, authorizeUrl);
    }

    @AccessLimit(seconds = 60, maxCount = 5)
    @GetMapping("/oauth")
    public ResultUtils loginOauth(@RequestParam(required = true) String code) {
        log.info("登录认证");
        String oauth = adService.loginOauth(code);
        return ResultUtils.Ok(null, oauth);
    }
}
