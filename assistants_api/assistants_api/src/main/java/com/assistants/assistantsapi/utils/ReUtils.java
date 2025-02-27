package com.assistants.assistantsapi.utils;

import java.util.regex.Pattern;

/**
 * @author Harris
 * @since 2024-05-23 17:03
 * 正则校验
 */
public class ReUtils {

    /**
     * 手机正则校验
     * @param phone 待校验手机号
     * @return bool
     */
    public static boolean rePhone(String phone) {
        Pattern pattern = Pattern.compile("1[3456789]\\d{9}");
        return pattern.matcher(phone).matches();
    }
}
