using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ACF RID: 2767
	[CLSCompliant(false)]
	public enum RPSErrorCode : uint
	{
		// Token: 0x04003422 RID: 13346
		PP_E_RPS_NOT_INITIALIZED = 2147783168U,
		// Token: 0x04003423 RID: 13347
		PP_E_RPS_FAILED_TO_CREATE_DOM,
		// Token: 0x04003424 RID: 13348
		PP_E_RPS_INTERNAL_ERROR,
		// Token: 0x04003425 RID: 13349
		PP_E_RPS_INVALID_OBJECT_ID,
		// Token: 0x04003426 RID: 13350
		PP_E_RPS_OBJECT_ID_CANNOT_OVERWRITE,
		// Token: 0x04003427 RID: 13351
		PP_E_RPS_FAILED_TO_TLS,
		// Token: 0x04003428 RID: 13352
		PP_E_RPS_XML_FILE_ERROR,
		// Token: 0x04003429 RID: 13353
		PP_E_RPS_READ_ONLY,
		// Token: 0x0400342A RID: 13354
		PP_E_RPS_SERVER_CONFIG_ALREADY_INITTED,
		// Token: 0x0400342B RID: 13355
		PP_E_RPS_INVALIDCONFIG,
		// Token: 0x0400342C RID: 13356
		PP_E_RPS_CERT_NOT_FOUND,
		// Token: 0x0400342D RID: 13357
		PP_E_RPS_SKIBUFFER_TOO_SMALL,
		// Token: 0x0400342E RID: 13358
		PP_E_RPS_FILE_TOO_LARGE,
		// Token: 0x0400342F RID: 13359
		PP_E_RPS_INVALID_DATATYPE,
		// Token: 0x04003430 RID: 13360
		PP_E_RPS_MORE_DATA,
		// Token: 0x04003431 RID: 13361
		PP_E_RPS_INVALID_SIGNATURE,
		// Token: 0x04003432 RID: 13362
		PP_E_RPS_ENCRYPTEDKEY_TOO_LARGE = 2147783185U,
		// Token: 0x04003433 RID: 13363
		PP_E_RPS_DATA_INTEGRITY_CHECK_FAILED,
		// Token: 0x04003434 RID: 13364
		PP_E_RPS_CERT_WITHOUT_PRIVATE_KEY = 2147783188U,
		// Token: 0x04003435 RID: 13365
		PP_E_RPS_NET_CONFIG_CACHE_ALREADY_INITTED,
		// Token: 0x04003436 RID: 13366
		PP_E_RPS_DOMAIN_ATTRIBUTE_NOT_FOUND,
		// Token: 0x04003437 RID: 13367
		PP_E_RPS_INVALIDDATA,
		// Token: 0x04003438 RID: 13368
		PP_E_RPS_TICKET_NOT_INITIALIZED,
		// Token: 0x04003439 RID: 13369
		PP_E_RPS_TICKET_CANNOT_BE_INITIALIZED_MORE_THAN_ONCE,
		// Token: 0x0400343A RID: 13370
		PP_E_RPS_SAML_ASSERTION_MISSINGDATA,
		// Token: 0x0400343B RID: 13371
		PP_E_RPS_INVALID_TIMEWINDOW,
		// Token: 0x0400343C RID: 13372
		PP_E_RPS_HTTP_BODY_REQUIRED = 2147783197U,
		// Token: 0x0400343D RID: 13373
		PP_E_RPS_INVALID_TICKET_TYPE,
		// Token: 0x0400343E RID: 13374
		PP_E_RPS_INVALID_SLIDINGWINDOW,
		// Token: 0x0400343F RID: 13375
		PP_E_RPS_REASON_INVALID_AUTHMETHOD,
		// Token: 0x04003440 RID: 13376
		PP_E_RPS_NO_SUCH_PROFILE_ATTRIBUTE = 2147783202U,
		// Token: 0x04003441 RID: 13377
		PP_E_RPS_INVALID_PROFILESCHEMA_TYPE,
		// Token: 0x04003442 RID: 13378
		PP_E_RPS_FAILED_DOWNLOAD,
		// Token: 0x04003443 RID: 13379
		PP_E_RPS_INVALID_SITEID = 2147783206U,
		// Token: 0x04003444 RID: 13380
		PP_E_RPS_BASE64DECODE_FAILED,
		// Token: 0x04003445 RID: 13381
		PP_E_RPS_REASON_TIMEWINDOW_EXPIRED,
		// Token: 0x04003446 RID: 13382
		PP_E_RPS_REASON_SLIDINGWINDOW_EXPIRED,
		// Token: 0x04003447 RID: 13383
		PP_E_RPS_CERT_INVALID_KEY_SPEC,
		// Token: 0x04003448 RID: 13384
		PP_E_RPS_INTERNAL_ERROR_CODE_UNSET_IN_EXCEPTION,
		// Token: 0x04003449 RID: 13385
		PP_E_RPS_REASON_INVALID_AUTHINSTANT_DATATYPE,
		// Token: 0x0400344A RID: 13386
		PP_E_RPS_REASON_HTTPS_OR_ENCRYPTED_TICKET_NEEDED,
		// Token: 0x0400344B RID: 13387
		PP_E_RPS_REASON_INCORRECT_IV_BYTES,
		// Token: 0x0400344C RID: 13388
		PP_E_RPS_REASON_PASSPORT_F_ERROR_ENCOUNTERED,
		// Token: 0x0400344D RID: 13389
		PP_E_RPS_NO_SESSION_KEY,
		// Token: 0x0400344E RID: 13390
		PP_E_RPS_INVALID_COOKIE_NAME,
		// Token: 0x0400344F RID: 13391
		PP_E_RPS_INVALID_AUTHPOLICY,
		// Token: 0x04003450 RID: 13392
		PP_E_RPS_INVALID_ENCRYPT_ALGID,
		// Token: 0x04003451 RID: 13393
		PP_E_RPS_REASON_POST_TICKET_TIMEWINDOW_EXPIRED,
		// Token: 0x04003452 RID: 13394
		PP_E_RPS_TICKET_HAS_NO_SESSIONKEY,
		// Token: 0x04003453 RID: 13395
		PP_E_RPS_TICKET_HAS_NO_CLIENTIP,
		// Token: 0x04003454 RID: 13396
		PP_E_RPS_REASON_INVALID_CLIENTIP_DATATYPE,
		// Token: 0x04003455 RID: 13397
		PP_E_RPS_REASON_IP_MISMATCH,
		// Token: 0x04003456 RID: 13398
		PP_E_RPS_PROXY_AUTH_NOT_ALLOWED,
		// Token: 0x04003457 RID: 13399
		PP_E_RPS_NOT_AUTHENTICATED,
		// Token: 0x04003458 RID: 13400
		PP_E_RPS_CERT_INVALID_ISSUER = 2147783232U,
		// Token: 0x04003459 RID: 13401
		PP_E_RPS_CERT_CA_ROLLOVER,
		// Token: 0x0400345A RID: 13402
		PP_E_RPS_CERT_INVALID_POP,
		// Token: 0x0400345B RID: 13403
		PP_E_RPS_CERT_NOT_VALID_FOR_MINTTL,
		// Token: 0x0400345C RID: 13404
		PP_E_RPS_INVALID_INPUT_STRING,
		// Token: 0x0400345D RID: 13405
		PP_E_RPS_TICKET_HAS_NO_OFFERACTIONS,
		// Token: 0x0400345E RID: 13406
		PP_E_RPS_REASON_INVALID_OFFERACTIONS_DATATYPE,
		// Token: 0x0400345F RID: 13407
		PP_E_RPS_REASON_OFFERACTIONS_INVALID,
		// Token: 0x04003460 RID: 13408
		PP_E_RPS_NO_OFFERACTIONS,
		// Token: 0x04003461 RID: 13409
		PP_E_RPS_SERVER_BUSY,
		// Token: 0x04003462 RID: 13410
		PP_E_RPS_REASON_INVALID_ISSUEINSTANT_DATATYPE = 2147783248U,
		// Token: 0x04003463 RID: 13411
		PP_E_RPS_REASON_INVALID_EXPIRYTIME_DATATYPE,
		// Token: 0x04003464 RID: 13412
		PP_E_RPS_REASON_INVALID_APPID_DATATYPE,
		// Token: 0x04003465 RID: 13413
		PP_E_RPS_OFFERACTION_MISMATCH,
		// Token: 0x04003466 RID: 13414
		PP_E_RPS_INVALID_WLID_TOKEN = 2147783254U,
		// Token: 0x04003467 RID: 13415
		PP_E_RPS_NEGO2_NOT_SUPPORTED,
		// Token: 0x04003468 RID: 13416
		PP_E_RPS_CUSTOMLIVEID_TICKET_TPE_NOT_SUPPORTED,
		// Token: 0x04003469 RID: 13417
		PP_E_RPS_SAML_TICKET_TYPE_NOT_SUPPORTED,
		// Token: 0x0400346A RID: 13418
		PP_E_RPSDATA_DATA_TOO_LARGE = 2147783680U,
		// Token: 0x0400346B RID: 13419
		PP_E_RPSDATA_INVALID_DATATYPE,
		// Token: 0x0400346C RID: 13420
		PP_E_RPSDATA_MORE_DATA,
		// Token: 0x0400346D RID: 13421
		PP_E_RPSDATA_INVALID_DATAOFFSET,
		// Token: 0x0400346E RID: 13422
		PP_E_RPSDATA_INVALIDDATA,
		// Token: 0x0400346F RID: 13423
		PP_E_RPSDATA_UNSUPPORTED_DATATYPE,
		// Token: 0x04003470 RID: 13424
		PP_E_RPS_UNDOCUMENTED = 2147762276U,
		// Token: 0x04003471 RID: 13425
		CRYPT_E_NOT_FOUND = 2148081668U,
		// Token: 0x04003472 RID: 13426
		CRYPT_E_NO_KEY_PROPERTY = 2148081675U,
		// Token: 0x04003473 RID: 13427
		CRYPT_E_NO_DECRYPT_CERT,
		// Token: 0x04003474 RID: 13428
		NTE_BAD_KEYSET = 2148073494U,
		// Token: 0x04003475 RID: 13429
		ERROR_NOT_ENOUGH_MEMORY = 2147942408U,
		// Token: 0x04003476 RID: 13430
		IO_OPERATION_ABORTED = 2147943395U
	}
}
