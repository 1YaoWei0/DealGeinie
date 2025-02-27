package com.assistants.assistantsapi.utils;

import cn.hutool.crypto.digest.DigestAlgorithm;
import cn.hutool.crypto.digest.DigestUtil;
import cn.hutool.crypto.digest.Digester;

/**
 * @author Harris
 * @since 2024-05-15 10:05
 */
public class Sha256Utils {


    /**
     * 单次
     * @param seed
     * @return
     */
    public static String sha256Digester(String seed) {
        Digester digester = DigestUtil.digester(DigestAlgorithm.SHA256);
        return digester.digestHex(seed);
    }

    /**
     * 双次
     * @param seed
     * @return
     */
    public static String sha256DoubleDigester(String seed) {
        String digester = sha256Digester(seed);
        return sha256Digester(digester);
    }
}
