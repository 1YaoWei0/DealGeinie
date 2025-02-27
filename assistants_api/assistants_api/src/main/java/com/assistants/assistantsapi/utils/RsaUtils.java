package com.assistants.assistantsapi.utils;

import cn.hutool.core.lang.UUID;
import com.auth0.jwk.Jwk;
import com.auth0.jwk.JwkException;
import com.auth0.jwk.JwkProvider;
import com.auth0.jwk.UrlJwkProvider;
import com.auth0.jwt.JWT;
import com.auth0.jwt.JWTVerifier;
import com.auth0.jwt.algorithms.Algorithm;
import com.auth0.jwt.exceptions.JWTVerificationException;
import com.auth0.jwt.exceptions.SignatureVerificationException;
import com.auth0.jwt.interfaces.Claim;
import com.auth0.jwt.interfaces.DecodedJWT;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;

import java.net.MalformedURLException;
import java.net.URL;
import java.security.interfaces.RSAPublicKey;
import java.util.Date;
import java.util.Map;

/**
 * @author Harris
 * @since 2024-05-14 16:52
 */
@Slf4j
public class RsaUtils {

    public static String tokenRsaVerifyMethod(String access_token, String jwks_uri) {
        log.info("JWT Validation Start.");
        DecodedJWT jwt = JWT.decode(access_token);
        try {
            URL keysURL = new URL(jwks_uri);
            JwkProvider provider = new UrlJwkProvider(keysURL);
            Jwk jwk = provider.get(jwt.getKeyId());
            Algorithm algorithm = Algorithm.RSA256((RSAPublicKey) jwk.getPublicKey(), null);
            algorithm.verify(jwt);
            log.info("JWT Validation End.");
        } catch (MalformedURLException e) {
            log.error("MalformedURLException：" + e.getMessage());
        } catch (JwkException e) {
            log.error("JwkException：" + e.getMessage());
        } catch (SignatureVerificationException e) {
            log.error("SignatureVerificationException：" + e.getMessage());
        }
        Map<String, Claim> claims = jwt.getClaims();
        return claims.get("upn").toString();
    }


    /**
     * 生成token
     *
     * @param val
     * @param sub register  login  message
     * @return
     */
    public static String createToken(String val, String sub) {
        return JWT.create().withIssuer("hm")
                .withSubject(sub)
                .withIssuedAt(new Date())
                .withExpiresAt(new Date(System.currentTimeMillis() + 60 * 1000))
                .withClaim("id", UUID.fastUUID().toString(false))
                .withClaim("uid", val)
                .sign(Algorithm.HMAC256(getTokenKey()));
    }

    /**
     * 验证token
     *
     * @param token
     * @param val
     * @return
     */
    public static boolean verifierToken(String token, String val) {

        try {
            JWTVerifier verifier = JWT.require(Algorithm.HMAC256(getTokenKey())).build();
            DecodedJWT jwt = verifier.verify(token);
            Date expiresAt = jwt.getExpiresAt();
            Date issuedAt = jwt.getIssuedAt();
            if (!(expiresAt.after(new Date()) && issuedAt.before(new Date()))) {
                return false;
            }
            String uid = jwt.getClaim("uid").asString();
            return StringUtils.equals(uid, val);
        } catch (IllegalArgumentException | JWTVerificationException e) {
            log.error(e.getMessage());
            return false;
        }
    }


    private static byte[] getTokenKey() {
        return "TJaldO29cbdTuRy8Zv5cMAXYV2iVvpsKfGKFo1kB6oqGyRuv5kA9lpKP9MFFpoGsXOvQ0E1t4TP5ZW0KR19qUUZrAgMBAAECggEALE7MB3DfIl3b/JiIvZgmvdbFyAcrc9XfX8uNRsGhLwGzA9YOghK64yZH4sLdTrnAJCB8prw6oNrD5DFk9IFra1T8p2b52Jkr6Bsy/xaIJXdU6gL8fRP5KhkTbrW3U7PEl0LIZmWkCWVon0Fp99bUVvJR7wZ6UwiL/IjhOTMI2W0JVwxTRCD8GsvbIzEEVLN4pN6gzwrtDFkECjdSblymxjsR3ii+Y86jRM2ELRtZXR6bEmgTVN6MT9s+m1GgQKJkimGQDqaVQxlcpC2SYSQql48Q4B09X6wjh2mkWtGYRujhgWmhtKBwnbv+Po5lEQ8M//BLprtAlPKm90PW1k0nGQKBgQDFE0YmMQ74DDJ++Euq/nWsThFg8vjDWnW/Nt8muKfBc/C3xHTTyaWVVgLd08PzJU/NHmto65Pq4fWSRntyMFFdwNxRnOqg9aKc9qIqrszB/DCxgtjDggGJm3Kq7r3TURX9biGljAB5aSKosyr8tK2KZ94NbwmYskkfRLwfRlkrHQKBgQDEY1XCm3inIAgvew0Nkah/5HIGX+SMnYv+Dr3AsqohXQemAVw3LqqiFD+3taDLlfsZeKbBJKIKZyYM4yWbEhOLW/bt/NomzkdWjb8Wk6SoevODwb08pOOsT3UmxXvRoibAIJdyIbeCblHgWGpHAr8Dl0kTvp0TuXqk1ZgSONd5JwKBgQC6OclbA5a6jIQW+Tg/n+7AYJEJhO/PYibMBuT9qRZUtuNsM8eV7gF7SWiyB48trz9me+1RmvMOzHEKxXGXaNMqfblzoY0RhqwgGOsK3Fz+nfnB".getBytes();
    }

}
