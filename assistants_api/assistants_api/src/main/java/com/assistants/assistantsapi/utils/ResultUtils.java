package com.assistants.assistantsapi.utils;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * @author Harris
 * @since 2024-05-24 13:21
 */

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ResultUtils {

    private Integer code=200;
    private String message;
    private Object data;

    public static ResultUtils CUSTOM(int code, String msg, Object data) {
        return new ResultUtils(code, msg, data);
    }

    public static ResultUtils Ok(String msg, Object data) {
        return new ResultUtils(200, msg, data);
    }

    public static ResultUtils Error(int code, String msg) {
        return new ResultUtils(code, msg, null);
    }

}
