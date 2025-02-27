package com.assistants.assistantsapi.utils;


/**
 * @author Harris
 * @since 2024-05-24 11:03
 */
public class RandomUtils {


    public static String randomSixNum(){
        return String.valueOf((int) (Math.random() * 1000000));
    }
}
