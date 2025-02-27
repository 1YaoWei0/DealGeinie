package com.assistants.assistantsapi.config;

import com.assistants.assistantsapi.interceptor.AccessLimtInterceptor;
import com.assistants.assistantsapi.interceptor.MyPreInterceptor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.InterceptorRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

/**
 * @author Harris
 * @since 2024-05-14 16:37
 */
@Configuration
public class WebConfig implements WebMvcConfigurer {
    @Autowired
    private MyPreInterceptor myInterceptor;
    @Autowired
    private AccessLimtInterceptor accessLimtInterceptor;

    @Override
    public void addCorsMappings(CorsRegistry registry) {
        registry.addMapping("/**")
                .allowCredentials(true)
                .allowedOriginPatterns("*")
                .allowedMethods("GET", "POST", "PUT", "DELETE")
                .allowedHeaders("*")
                .maxAge(3600);
    }

    @Override
    public void addInterceptors(InterceptorRegistry registry) {
        registry.addInterceptor(accessLimtInterceptor).addPathPatterns("/**");
        //registry.addInterceptor(myInterceptor).addPathPatterns("/**");
    }
}
