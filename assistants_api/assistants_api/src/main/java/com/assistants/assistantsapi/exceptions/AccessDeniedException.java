package com.assistants.assistantsapi.exceptions;

/**
 * @author Harris
 * @since 2024-05-24 17:55
 */
public class AccessDeniedException extends RuntimeException{
    private Integer code;
    private String msg;

    public Integer getCode() {
        return code;
    }

    public void setCode(Integer code) {
        this.code = code;
    }

    public String getMsg() {
        return msg;
    }

    public void setMsg(String msg) {
        this.msg = msg;
    }

    public AccessDeniedException(Integer errorCode, String errorMsg) {
        super(String.valueOf(errorCode));
        this.code = errorCode;
        this.msg = errorMsg;
    }
}
