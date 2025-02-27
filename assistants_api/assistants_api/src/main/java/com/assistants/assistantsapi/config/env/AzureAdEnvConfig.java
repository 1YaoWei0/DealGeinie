package com.assistants.assistantsapi.config.env;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

/**
 * @author Harris
 * @since 2024-05-14 16:11
 */
@Data
@Component
@ConfigurationProperties(prefix = "azure-ad")
public class AzureAdEnvConfig {
    private String clientId;
    private String tenantId;
    private String scope;
    private String clientSecret;
    private String resource;
    private String redirectUri;
    private String authorizeUrl;
    private String tokenUrl;
    private String tokenUserUrl;
    private String openidConnectUrl;
    private String allUserinfoUrl;
}
