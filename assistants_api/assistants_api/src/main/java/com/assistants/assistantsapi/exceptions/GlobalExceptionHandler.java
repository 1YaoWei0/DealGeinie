package com.assistants.assistantsapi.exceptions;

import com.assistants.assistantsapi.utils.ResultUtils;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;

/**
 * @author Harris
 * @since 2024-05-24 18:01
 */
@Slf4j
@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(value = AccessDeniedException.class)
    @ResponseBody
    public ResultUtils bizExceptionHandler(HttpServletRequest req, AccessDeniedException e) {
        log.error("发生业务异常！原因是：{}", e.getMsg());
        return ResultUtils.Error(e.getCode(), e.getMsg());
    }
}
