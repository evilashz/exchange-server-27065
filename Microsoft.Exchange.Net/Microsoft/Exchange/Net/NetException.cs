using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000DC RID: 220
	internal static class NetException
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x000142D4 File Offset: 0x000124D4
		static NetException()
		{
			NetException.stringIDs.Add(2888966445U, "NotFederated");
			NetException.stringIDs.Add(3146200830U, "InvalidMaxBufferCount");
			NetException.stringIDs.Add(2620764921U, "InvalidWmaFormat");
			NetException.stringIDs.Add(4272875403U, "SmallCapacity");
			NetException.stringIDs.Add(1883331241U, "LargeBuffer");
			NetException.stringIDs.Add(4266227918U, "BufferOverflow");
			NetException.stringIDs.Add(2543924840U, "ProcessRunnerTimeout");
			NetException.stringIDs.Add(774122735U, "DSSAndRSA");
			NetException.stringIDs.Add(222712050U, "AuthzUnableToDoAccessCheckFromNullOrInvalidHandle");
			NetException.stringIDs.Add(4239692632U, "EmptyServerList");
			NetException.stringIDs.Add(1300380106U, "IAsyncResultMismatch");
			NetException.stringIDs.Add(4130279271U, "UnknownPropertyType");
			NetException.stringIDs.Add(724957639U, "FindLineError");
			NetException.stringIDs.Add(1939069342U, "InvalidDateValue");
			NetException.stringIDs.Add(2078991984U, "LargeParameter");
			NetException.stringIDs.Add(2642084527U, "AuthzIdentityNotSet");
			NetException.stringIDs.Add(335518370U, "AuthManagerNotInitialized");
			NetException.stringIDs.Add(3109722150U, "ApplicationUriMissing");
			NetException.stringIDs.Add(3853787433U, "CorruptStringSize");
			NetException.stringIDs.Add(523155784U, "DuplicateItem");
			NetException.stringIDs.Add(3497416644U, "BufferMismatch");
			NetException.stringIDs.Add(282247621U, "CannotHandleUnsecuredRedirection");
			NetException.stringIDs.Add(2788838128U, "EndAlreadyCalled");
			NetException.stringIDs.Add(1998366202U, "CollectionChanged");
			NetException.stringIDs.Add(3363535402U, "BadOffset");
			NetException.stringIDs.Add(1479752888U, "AlreadyInitialized");
			NetException.stringIDs.Add(4293010739U, "SignatureDoesNotMatch");
			NetException.stringIDs.Add(1000630245U, "NoTokenContext");
			NetException.stringIDs.Add(574942079U, "InvalidUnicodeString");
			NetException.stringIDs.Add(2031043979U, "DomainsMissing");
			NetException.stringIDs.Add(4205481756U, "OnlySSLSupported");
			NetException.stringIDs.Add(1391971810U, "StoreTypeUnsupported");
			NetException.stringIDs.Add(1787132391U, "MultipleOfAlignmentFactor");
			NetException.stringIDs.Add(3739156314U, "GetUserSettingsGeneralFailure");
			NetException.stringIDs.Add(3669818989U, "NoResponseFromHttpServer");
			NetException.stringIDs.Add(4039752388U, "NoContext");
			NetException.stringIDs.Add(1520602905U, "NegativeIndex");
			NetException.stringIDs.Add(3658473395U, "SmallBuffer");
			NetException.stringIDs.Add(2711616649U, "ReceiveInProgress");
			NetException.stringIDs.Add(994785501U, "NotInitialized");
			NetException.stringIDs.Add(2115127879U, "InvalidDuration");
			NetException.stringIDs.Add(1323030597U, "EmptyCertSubject");
			NetException.stringIDs.Add(3037031059U, "UnknownAuthMechanism");
			NetException.stringIDs.Add(2092010860U, "SeekOrigin");
			NetException.stringIDs.Add(2702236218U, "aadTransportFailureException");
			NetException.stringIDs.Add(1228304272U, "EmptyFQDNList");
			NetException.stringIDs.Add(2649873638U, "ResolveInProgress");
			NetException.stringIDs.Add(4160234858U, "EmptyCertThumbprint");
			NetException.stringIDs.Add(3484386229U, "AuthzInitializeContextFromDuplicateAuthZFailed");
			NetException.stringIDs.Add(1344176929U, "StringContainsInvalidCharacters");
			NetException.stringIDs.Add(1018641786U, "UnexpectedUserResponses");
			NetException.stringIDs.Add(2422771817U, "LogonData");
			NetException.stringIDs.Add(1024372311U, "AuthFailureException");
			NetException.stringIDs.Add(2234054046U, "ImmutableStream");
			NetException.stringIDs.Add(3706950651U, "NegativeCapacity");
			NetException.stringIDs.Add(3076215569U, "UnsupportedFilter");
			NetException.stringIDs.Add(1610756644U, "InvalidIPType");
			NetException.stringIDs.Add(3156409200U, "TlsProtocolFailureException");
			NetException.stringIDs.Add(1791693013U, "CorruptArraySize");
			NetException.stringIDs.Add(1612735551U, "AuthzGetInformationFromContextReturnedSuccessForSize");
			NetException.stringIDs.Add(2043762708U, "CapacityOverflow");
			NetException.stringIDs.Add(1871965737U, "ExportAndArchive");
			NetException.stringIDs.Add(1441393770U, "InternalOperationFailure");
			NetException.stringIDs.Add(3175999590U, "MissingNullTerminator");
			NetException.stringIDs.Add(2134260535U, "DSSNotSupported");
			NetException.stringIDs.Add(2821016632U, "InvalidNumberOfBytes");
			NetException.stringIDs.Add(2396394977U, "CouldNotAllocateFragment");
			NetException.stringIDs.Add(565660828U, "OutOfRange");
			NetException.stringIDs.Add(115723314U, "ResponseMissingErrorCode");
			NetException.stringIDs.Add(1151742595U, "IllegalContainedAccess");
			NetException.stringIDs.Add(2305890504U, "UseReportEncryptedBytesFilled");
			NetException.stringIDs.Add(4177743836U, "SendInProgress");
			NetException.stringIDs.Add(976065123U, "LargeIndex");
			NetException.stringIDs.Add(1274073424U, "InvalidSize");
			NetException.stringIDs.Add(1896894556U, "EmptyResponse");
			NetException.stringIDs.Add(1753773471U, "CollectionEmpty");
			NetException.stringIDs.Add(2455122056U, "NegativeParameter");
			NetException.stringIDs.Add(907185727U, "MustBeTlsForAuthException");
			NetException.stringIDs.Add(1927799671U, "WrongType");
			NetException.stringIDs.Add(1586250006U, "DataContainsBareLinefeeds");
			NetException.stringIDs.Add(1945685814U, "ClosedStream");
			NetException.stringIDs.Add(2499062057U, "MissingPrimaryGroupSid");
			NetException.stringIDs.Add(1163610383U, "ReadPastEnd");
			NetException.stringIDs.Add(1890952107U, "TlsAlreadyNegotiated");
			NetException.stringIDs.Add(2126643456U, "DestinationIndexOutOfRange");
			NetException.stringIDs.Add(3093826520U, "TruncatedData");
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000149C8 File Offset: 0x00012BC8
		public static LocalizedString NotFederated
		{
			get
			{
				return new LocalizedString("NotFederated", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000149E6 File Offset: 0x00012BE6
		public static LocalizedString InvalidMaxBufferCount
		{
			get
			{
				return new LocalizedString("InvalidMaxBufferCount", "ExBFC10C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014A04 File Offset: 0x00012C04
		public static LocalizedString AuthzUnableToGetTokenFromNullOrInvalidHandle(string clientContext)
		{
			return new LocalizedString("AuthzUnableToGetTokenFromNullOrInvalidHandle", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00014A33 File Offset: 0x00012C33
		public static LocalizedString InvalidWmaFormat
		{
			get
			{
				return new LocalizedString("InvalidWmaFormat", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00014A51 File Offset: 0x00012C51
		public static LocalizedString SmallCapacity
		{
			get
			{
				return new LocalizedString("SmallCapacity", "ExA4724A", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014A70 File Offset: 0x00012C70
		public static LocalizedString TlsApiFailureException(string error)
		{
			return new LocalizedString("TlsApiFailureException", "", false, false, NetException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00014A9F File Offset: 0x00012C9F
		public static LocalizedString LargeBuffer
		{
			get
			{
				return new LocalizedString("LargeBuffer", "ExCA1CF4", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00014ABD File Offset: 0x00012CBD
		public static LocalizedString BufferOverflow
		{
			get
			{
				return new LocalizedString("BufferOverflow", "ExB97FC1", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014ADC File Offset: 0x00012CDC
		public static LocalizedString CertificateSubjectNotFound(string name)
		{
			return new LocalizedString("CertificateSubjectNotFound", "", false, false, NetException.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00014B0B File Offset: 0x00012D0B
		public static LocalizedString ProcessRunnerTimeout
		{
			get
			{
				return new LocalizedString("ProcessRunnerTimeout", "ExEBFF7D", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00014B29 File Offset: 0x00012D29
		public static LocalizedString DSSAndRSA
		{
			get
			{
				return new LocalizedString("DSSAndRSA", "Ex72FA3A", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00014B47 File Offset: 0x00012D47
		public static LocalizedString AuthzUnableToDoAccessCheckFromNullOrInvalidHandle
		{
			get
			{
				return new LocalizedString("AuthzUnableToDoAccessCheckFromNullOrInvalidHandle", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014B65 File Offset: 0x00012D65
		public static LocalizedString EmptyServerList
		{
			get
			{
				return new LocalizedString("EmptyServerList", "Ex295FBA", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00014B83 File Offset: 0x00012D83
		public static LocalizedString IAsyncResultMismatch
		{
			get
			{
				return new LocalizedString("IAsyncResultMismatch", "Ex38C37F", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014BA1 File Offset: 0x00012DA1
		public static LocalizedString UnknownPropertyType
		{
			get
			{
				return new LocalizedString("UnknownPropertyType", "ExB8CF30", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00014BBF File Offset: 0x00012DBF
		public static LocalizedString FindLineError
		{
			get
			{
				return new LocalizedString("FindLineError", "ExE043FA", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014BE0 File Offset: 0x00012DE0
		public static LocalizedString InvalidSmtpServerResponseException(string response)
		{
			return new LocalizedString("InvalidSmtpServerResponseException", "", false, false, NetException.ResourceManager, new object[]
			{
				response
			});
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014C10 File Offset: 0x00012E10
		public static LocalizedString UnmanagedAlloc(int size)
		{
			return new LocalizedString("UnmanagedAlloc", "ExF11D00", false, true, NetException.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00014C44 File Offset: 0x00012E44
		public static LocalizedString InvalidDateValue
		{
			get
			{
				return new LocalizedString("InvalidDateValue", "ExADE97C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00014C64 File Offset: 0x00012E64
		public static LocalizedString InvalidWaveFormat(string s)
		{
			return new LocalizedString("InvalidWaveFormat", "", false, false, NetException.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00014C94 File Offset: 0x00012E94
		public static LocalizedString DomainNotPresentInResponse(string domain, string domainsFromResponse)
		{
			return new LocalizedString("DomainNotPresentInResponse", "", false, false, NetException.ResourceManager, new object[]
			{
				domain,
				domainsFromResponse
			});
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014CC7 File Offset: 0x00012EC7
		public static LocalizedString LargeParameter
		{
			get
			{
				return new LocalizedString("LargeParameter", "Ex5EFE8D", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014CE8 File Offset: 0x00012EE8
		public static LocalizedString UnexpectedAutodiscoverResult(string result)
		{
			return new LocalizedString("UnexpectedAutodiscoverResult", "", false, false, NetException.ResourceManager, new object[]
			{
				result
			});
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014D18 File Offset: 0x00012F18
		public static LocalizedString AuthzUnableToPerformAccessCheck(string clientContext)
		{
			return new LocalizedString("AuthzUnableToPerformAccessCheck", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00014D47 File Offset: 0x00012F47
		public static LocalizedString AuthzIdentityNotSet
		{
			get
			{
				return new LocalizedString("AuthzIdentityNotSet", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00014D65 File Offset: 0x00012F65
		public static LocalizedString AuthManagerNotInitialized
		{
			get
			{
				return new LocalizedString("AuthManagerNotInitialized", "Ex2FF5B8", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00014D83 File Offset: 0x00012F83
		public static LocalizedString ApplicationUriMissing
		{
			get
			{
				return new LocalizedString("ApplicationUriMissing", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00014DA1 File Offset: 0x00012FA1
		public static LocalizedString CorruptStringSize
		{
			get
			{
				return new LocalizedString("CorruptStringSize", "Ex3C16B8", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00014DBF File Offset: 0x00012FBF
		public static LocalizedString DuplicateItem
		{
			get
			{
				return new LocalizedString("DuplicateItem", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00014DE0 File Offset: 0x00012FE0
		public static LocalizedString MalformedLocationHeader(string locationHeader)
		{
			return new LocalizedString("MalformedLocationHeader", "", false, false, NetException.ResourceManager, new object[]
			{
				locationHeader
			});
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00014E10 File Offset: 0x00013010
		public static LocalizedString UnexpectedSmtpServerResponseException(int expectedResponseCode, int actualResponseCode, string wholeResponse)
		{
			return new LocalizedString("UnexpectedSmtpServerResponseException", "", false, false, NetException.ResourceManager, new object[]
			{
				expectedResponseCode,
				actualResponseCode,
				wholeResponse
			});
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00014E54 File Offset: 0x00013054
		public static LocalizedString FailedToConnectToSMTPServerException(string server)
		{
			return new LocalizedString("FailedToConnectToSMTPServerException", "", false, false, NetException.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00014E83 File Offset: 0x00013083
		public static LocalizedString BufferMismatch
		{
			get
			{
				return new LocalizedString("BufferMismatch", "ExBFABE7", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00014EA4 File Offset: 0x000130A4
		public static LocalizedString LogonAsNetworkServiceFailed(string error)
		{
			return new LocalizedString("LogonAsNetworkServiceFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00014ED3 File Offset: 0x000130D3
		public static LocalizedString CannotHandleUnsecuredRedirection
		{
			get
			{
				return new LocalizedString("CannotHandleUnsecuredRedirection", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014EF1 File Offset: 0x000130F1
		public static LocalizedString EndAlreadyCalled
		{
			get
			{
				return new LocalizedString("EndAlreadyCalled", "ExF0F933", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00014F0F File Offset: 0x0001310F
		public static LocalizedString CollectionChanged
		{
			get
			{
				return new LocalizedString("CollectionChanged", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00014F30 File Offset: 0x00013130
		public static LocalizedString InvalidSid(string invalidSid)
		{
			return new LocalizedString("InvalidSid", "ExC35A6F", false, true, NetException.ResourceManager, new object[]
			{
				invalidSid
			});
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00014F60 File Offset: 0x00013160
		public static LocalizedString UnsupportedAudioFormat(string fileName)
		{
			return new LocalizedString("UnsupportedAudioFormat", "", false, false, NetException.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00014F8F File Offset: 0x0001318F
		public static LocalizedString BadOffset
		{
			get
			{
				return new LocalizedString("BadOffset", "Ex78B5D0", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00014FAD File Offset: 0x000131AD
		public static LocalizedString AlreadyInitialized
		{
			get
			{
				return new LocalizedString("AlreadyInitialized", "Ex757617", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00014FCB File Offset: 0x000131CB
		public static LocalizedString SignatureDoesNotMatch
		{
			get
			{
				return new LocalizedString("SignatureDoesNotMatch", "Ex01B475", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00014FE9 File Offset: 0x000131E9
		public static LocalizedString NoTokenContext
		{
			get
			{
				return new LocalizedString("NoTokenContext", "Ex41300D", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00015007 File Offset: 0x00013207
		public static LocalizedString InvalidUnicodeString
		{
			get
			{
				return new LocalizedString("InvalidUnicodeString", "ExEA88A8", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00015025 File Offset: 0x00013225
		public static LocalizedString DomainsMissing
		{
			get
			{
				return new LocalizedString("DomainsMissing", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00015044 File Offset: 0x00013244
		public static LocalizedString AudioConversionFailed(string reason)
		{
			return new LocalizedString("AudioConversionFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00015073 File Offset: 0x00013273
		public static LocalizedString OnlySSLSupported
		{
			get
			{
				return new LocalizedString("OnlySSLSupported", "Ex96B89E", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00015094 File Offset: 0x00013294
		public static LocalizedString NotConnectedToSMTPServerException(string server)
		{
			return new LocalizedString("NotConnectedToSMTPServerException", "", false, false, NetException.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000150C3 File Offset: 0x000132C3
		public static LocalizedString StoreTypeUnsupported
		{
			get
			{
				return new LocalizedString("StoreTypeUnsupported", "ExE9E5F7", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000150E1 File Offset: 0x000132E1
		public static LocalizedString MultipleOfAlignmentFactor
		{
			get
			{
				return new LocalizedString("MultipleOfAlignmentFactor", "ExD313AE", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000150FF File Offset: 0x000132FF
		public static LocalizedString GetUserSettingsGeneralFailure
		{
			get
			{
				return new LocalizedString("GetUserSettingsGeneralFailure", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001511D File Offset: 0x0001331D
		public static LocalizedString NoResponseFromHttpServer
		{
			get
			{
				return new LocalizedString("NoResponseFromHttpServer", "Ex61FC70", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001513B File Offset: 0x0001333B
		public static LocalizedString NoContext
		{
			get
			{
				return new LocalizedString("NoContext", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00015159 File Offset: 0x00013359
		public static LocalizedString NegativeIndex
		{
			get
			{
				return new LocalizedString("NegativeIndex", "ExB4DA6B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00015177 File Offset: 0x00013377
		public static LocalizedString SmallBuffer
		{
			get
			{
				return new LocalizedString("SmallBuffer", "Ex69366B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015195 File Offset: 0x00013395
		public static LocalizedString ReceiveInProgress
		{
			get
			{
				return new LocalizedString("ReceiveInProgress", "ExFF7646", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000151B4 File Offset: 0x000133B4
		public static LocalizedString AuthzInitializeContextFromTokenFailed(string clientContext)
		{
			return new LocalizedString("AuthzInitializeContextFromTokenFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000151E3 File Offset: 0x000133E3
		public static LocalizedString NotInitialized
		{
			get
			{
				return new LocalizedString("NotInitialized", "ExC4DE2C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00015201 File Offset: 0x00013401
		public static LocalizedString InvalidDuration
		{
			get
			{
				return new LocalizedString("InvalidDuration", "Ex883DF3", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015220 File Offset: 0x00013420
		public static LocalizedString InvalidFlags(int flags)
		{
			return new LocalizedString("InvalidFlags", "Ex43D52A", false, true, NetException.ResourceManager, new object[]
			{
				flags
			});
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00015254 File Offset: 0x00013454
		public static LocalizedString AuthzGetInformationFromContextFailed(string clientContext)
		{
			return new LocalizedString("AuthzGetInformationFromContextFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00015284 File Offset: 0x00013484
		public static LocalizedString AuthApiFailureException(string error)
		{
			return new LocalizedString("AuthApiFailureException", "", false, false, NetException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000152B3 File Offset: 0x000134B3
		public static LocalizedString EmptyCertSubject
		{
			get
			{
				return new LocalizedString("EmptyCertSubject", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000152D4 File Offset: 0x000134D4
		public static LocalizedString AuthzInitializeContextFromSidFailed(string clientContext)
		{
			return new LocalizedString("AuthzInitializeContextFromSidFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015303 File Offset: 0x00013503
		public static LocalizedString UnknownAuthMechanism
		{
			get
			{
				return new LocalizedString("UnknownAuthMechanism", "ExB2EA4B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00015321 File Offset: 0x00013521
		public static LocalizedString SeekOrigin
		{
			get
			{
				return new LocalizedString("SeekOrigin", "Ex83058E", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001533F File Offset: 0x0001353F
		public static LocalizedString aadTransportFailureException
		{
			get
			{
				return new LocalizedString("aadTransportFailureException", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001535D File Offset: 0x0001355D
		public static LocalizedString EmptyFQDNList
		{
			get
			{
				return new LocalizedString("EmptyFQDNList", "Ex995D96", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001537B File Offset: 0x0001357B
		public static LocalizedString ResolveInProgress
		{
			get
			{
				return new LocalizedString("ResolveInProgress", "Ex9C21C2", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00015399 File Offset: 0x00013599
		public static LocalizedString EmptyCertThumbprint
		{
			get
			{
				return new LocalizedString("EmptyCertThumbprint", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000153B7 File Offset: 0x000135B7
		public static LocalizedString AuthzInitializeContextFromDuplicateAuthZFailed
		{
			get
			{
				return new LocalizedString("AuthzInitializeContextFromDuplicateAuthZFailed", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000153D5 File Offset: 0x000135D5
		public static LocalizedString StringContainsInvalidCharacters
		{
			get
			{
				return new LocalizedString("StringContainsInvalidCharacters", "ExD765BA", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000153F3 File Offset: 0x000135F3
		public static LocalizedString UnexpectedUserResponses
		{
			get
			{
				return new LocalizedString("UnexpectedUserResponses", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015414 File Offset: 0x00013614
		public static LocalizedString UnexpectedStatusCode(string statusCode)
		{
			return new LocalizedString("UnexpectedStatusCode", "", false, false, NetException.ResourceManager, new object[]
			{
				statusCode
			});
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00015443 File Offset: 0x00013643
		public static LocalizedString LogonData
		{
			get
			{
				return new LocalizedString("LogonData", "ExBF27A7", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00015461 File Offset: 0x00013661
		public static LocalizedString AuthFailureException
		{
			get
			{
				return new LocalizedString("AuthFailureException", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001547F File Offset: 0x0001367F
		public static LocalizedString ImmutableStream
		{
			get
			{
				return new LocalizedString("ImmutableStream", "Ex545DFA", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001549D File Offset: 0x0001369D
		public static LocalizedString NegativeCapacity
		{
			get
			{
				return new LocalizedString("NegativeCapacity", "Ex28A2C5", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000154BB File Offset: 0x000136BB
		public static LocalizedString UnsupportedFilter
		{
			get
			{
				return new LocalizedString("UnsupportedFilter", "Ex0D5214", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000154D9 File Offset: 0x000136D9
		public static LocalizedString InvalidIPType
		{
			get
			{
				return new LocalizedString("InvalidIPType", "Ex261EBD", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000154F7 File Offset: 0x000136F7
		public static LocalizedString TlsProtocolFailureException
		{
			get
			{
				return new LocalizedString("TlsProtocolFailureException", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00015515 File Offset: 0x00013715
		public static LocalizedString CorruptArraySize
		{
			get
			{
				return new LocalizedString("CorruptArraySize", "ExDE1375", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00015533 File Offset: 0x00013733
		public static LocalizedString AuthzGetInformationFromContextReturnedSuccessForSize
		{
			get
			{
				return new LocalizedString("AuthzGetInformationFromContextReturnedSuccessForSize", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00015551 File Offset: 0x00013751
		public static LocalizedString CapacityOverflow
		{
			get
			{
				return new LocalizedString("CapacityOverflow", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001556F File Offset: 0x0001376F
		public static LocalizedString ExportAndArchive
		{
			get
			{
				return new LocalizedString("ExportAndArchive", "Ex54F1AE", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015590 File Offset: 0x00013790
		public static LocalizedString UnexpectedResponseType(string responseType)
		{
			return new LocalizedString("UnexpectedResponseType", "", false, false, NetException.ResourceManager, new object[]
			{
				responseType
			});
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000155BF File Offset: 0x000137BF
		public static LocalizedString InternalOperationFailure
		{
			get
			{
				return new LocalizedString("InternalOperationFailure", "ExD62529", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000155DD File Offset: 0x000137DD
		public static LocalizedString MissingNullTerminator
		{
			get
			{
				return new LocalizedString("MissingNullTerminator", "ExA381A7", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000155FB File Offset: 0x000137FB
		public static LocalizedString DSSNotSupported
		{
			get
			{
				return new LocalizedString("DSSNotSupported", "Ex610A51", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001561C File Offset: 0x0001381C
		public static LocalizedString CertificateThumbprintNotFound(string name)
		{
			return new LocalizedString("CertificateThumbprintNotFound", "", false, false, NetException.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001564B File Offset: 0x0001384B
		public static LocalizedString InvalidNumberOfBytes
		{
			get
			{
				return new LocalizedString("InvalidNumberOfBytes", "Ex186A41", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00015669 File Offset: 0x00013869
		public static LocalizedString CouldNotAllocateFragment
		{
			get
			{
				return new LocalizedString("CouldNotAllocateFragment", "ExC1DDAE", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00015687 File Offset: 0x00013887
		public static LocalizedString OutOfRange
		{
			get
			{
				return new LocalizedString("OutOfRange", "ExEA934B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000156A8 File Offset: 0x000138A8
		public static LocalizedString DiscoveryFailed(string domain)
		{
			return new LocalizedString("DiscoveryFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000156D8 File Offset: 0x000138D8
		public static LocalizedString UnsupportedAuthMechanismException(string authMechanism)
		{
			return new LocalizedString("UnsupportedAuthMechanismException", "", false, false, NetException.ResourceManager, new object[]
			{
				authMechanism
			});
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00015708 File Offset: 0x00013908
		public static LocalizedString InvalidUserForGetUserSettings(string user)
		{
			return new LocalizedString("InvalidUserForGetUserSettings", "", false, false, NetException.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00015738 File Offset: 0x00013938
		public static LocalizedString InvalidFQDN(string name)
		{
			return new LocalizedString("InvalidFQDN", "Ex260214", false, true, NetException.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00015768 File Offset: 0x00013968
		public static LocalizedString CertificateIssuerNotfound(string name)
		{
			return new LocalizedString("CertificateIssuerNotfound", "", false, false, NetException.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00015798 File Offset: 0x00013998
		public static LocalizedString AlreadyConnectedToSMTPServerException(string server)
		{
			return new LocalizedString("AlreadyConnectedToSMTPServerException", "", false, false, NetException.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000157C7 File Offset: 0x000139C7
		public static LocalizedString ResponseMissingErrorCode
		{
			get
			{
				return new LocalizedString("ResponseMissingErrorCode", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000157E5 File Offset: 0x000139E5
		public static LocalizedString IllegalContainedAccess
		{
			get
			{
				return new LocalizedString("IllegalContainedAccess", "ExF09B3E", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00015804 File Offset: 0x00013A04
		public static LocalizedString ErrorInResponse(string errorCode)
		{
			return new LocalizedString("ErrorInResponse", "", false, false, NetException.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00015833 File Offset: 0x00013A33
		public static LocalizedString UseReportEncryptedBytesFilled
		{
			get
			{
				return new LocalizedString("UseReportEncryptedBytesFilled", "ExA6BD4C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00015851 File Offset: 0x00013A51
		public static LocalizedString SendInProgress
		{
			get
			{
				return new LocalizedString("SendInProgress", "ExC9F7D4", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001586F File Offset: 0x00013A6F
		public static LocalizedString LargeIndex
		{
			get
			{
				return new LocalizedString("LargeIndex", "ExA3051C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001588D File Offset: 0x00013A8D
		public static LocalizedString InvalidSize
		{
			get
			{
				return new LocalizedString("InvalidSize", "ExC7703B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000158AB File Offset: 0x00013AAB
		public static LocalizedString EmptyResponse
		{
			get
			{
				return new LocalizedString("EmptyResponse", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000158CC File Offset: 0x00013ACC
		public static LocalizedString WildcardNotSupported(string name)
		{
			return new LocalizedString("WildcardNotSupported", "Ex2F5706", false, true, NetException.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000158FC File Offset: 0x00013AFC
		public static LocalizedString NonHttpsLocationHeader(string locationHeader)
		{
			return new LocalizedString("NonHttpsLocationHeader", "", false, false, NetException.ResourceManager, new object[]
			{
				locationHeader
			});
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001592B File Offset: 0x00013B2B
		public static LocalizedString CollectionEmpty
		{
			get
			{
				return new LocalizedString("CollectionEmpty", "Ex76100C", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00015949 File Offset: 0x00013B49
		public static LocalizedString NegativeParameter
		{
			get
			{
				return new LocalizedString("NegativeParameter", "ExAC35B0", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00015967 File Offset: 0x00013B67
		public static LocalizedString MustBeTlsForAuthException
		{
			get
			{
				return new LocalizedString("MustBeTlsForAuthException", "", false, false, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00015988 File Offset: 0x00013B88
		public static LocalizedString UnexpectedPathLocationHeader(string locationHeader)
		{
			return new LocalizedString("UnexpectedPathLocationHeader", "", false, false, NetException.ResourceManager, new object[]
			{
				locationHeader
			});
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000159B7 File Offset: 0x00013BB7
		public static LocalizedString WrongType
		{
			get
			{
				return new LocalizedString("WrongType", "Ex05E06B", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000159D8 File Offset: 0x00013BD8
		public static LocalizedString ApplicationUriMalformed(string applicaitonUri)
		{
			return new LocalizedString("ApplicationUriMalformed", "", false, false, NetException.ResourceManager, new object[]
			{
				applicaitonUri
			});
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00015A07 File Offset: 0x00013C07
		public static LocalizedString DataContainsBareLinefeeds
		{
			get
			{
				return new LocalizedString("DataContainsBareLinefeeds", "ExC3EF57", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00015A28 File Offset: 0x00013C28
		public static LocalizedString AuthzAddSidsToContextFailed(string clientContext)
		{
			return new LocalizedString("AuthzAddSidsToContextFailed", "", false, false, NetException.ResourceManager, new object[]
			{
				clientContext
			});
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00015A57 File Offset: 0x00013C57
		public static LocalizedString ClosedStream
		{
			get
			{
				return new LocalizedString("ClosedStream", "Ex1EDA64", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00015A75 File Offset: 0x00013C75
		public static LocalizedString MissingPrimaryGroupSid
		{
			get
			{
				return new LocalizedString("MissingPrimaryGroupSid", "ExF7732F", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00015A93 File Offset: 0x00013C93
		public static LocalizedString ReadPastEnd
		{
			get
			{
				return new LocalizedString("ReadPastEnd", "Ex17A55D", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00015AB1 File Offset: 0x00013CB1
		public static LocalizedString TlsAlreadyNegotiated
		{
			get
			{
				return new LocalizedString("TlsAlreadyNegotiated", "Ex769CCF", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00015ACF File Offset: 0x00013CCF
		public static LocalizedString DestinationIndexOutOfRange
		{
			get
			{
				return new LocalizedString("DestinationIndexOutOfRange", "Ex24A377", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00015AED File Offset: 0x00013CED
		public static LocalizedString TruncatedData
		{
			get
			{
				return new LocalizedString("TruncatedData", "Ex613FB4", false, true, NetException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00015B0B File Offset: 0x00013D0B
		public static LocalizedString GetLocalizedString(NetException.IDs key)
		{
			return new LocalizedString(NetException.stringIDs[(uint)key], NetException.ResourceManager, new object[0]);
		}

		// Token: 0x04000473 RID: 1139
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(86);

		// Token: 0x04000474 RID: 1140
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.NetException", typeof(NetException).GetTypeInfo().Assembly);

		// Token: 0x020000DD RID: 221
		public enum IDs : uint
		{
			// Token: 0x04000476 RID: 1142
			NotFederated = 2888966445U,
			// Token: 0x04000477 RID: 1143
			InvalidMaxBufferCount = 3146200830U,
			// Token: 0x04000478 RID: 1144
			InvalidWmaFormat = 2620764921U,
			// Token: 0x04000479 RID: 1145
			SmallCapacity = 4272875403U,
			// Token: 0x0400047A RID: 1146
			LargeBuffer = 1883331241U,
			// Token: 0x0400047B RID: 1147
			BufferOverflow = 4266227918U,
			// Token: 0x0400047C RID: 1148
			ProcessRunnerTimeout = 2543924840U,
			// Token: 0x0400047D RID: 1149
			DSSAndRSA = 774122735U,
			// Token: 0x0400047E RID: 1150
			AuthzUnableToDoAccessCheckFromNullOrInvalidHandle = 222712050U,
			// Token: 0x0400047F RID: 1151
			EmptyServerList = 4239692632U,
			// Token: 0x04000480 RID: 1152
			IAsyncResultMismatch = 1300380106U,
			// Token: 0x04000481 RID: 1153
			UnknownPropertyType = 4130279271U,
			// Token: 0x04000482 RID: 1154
			FindLineError = 724957639U,
			// Token: 0x04000483 RID: 1155
			InvalidDateValue = 1939069342U,
			// Token: 0x04000484 RID: 1156
			LargeParameter = 2078991984U,
			// Token: 0x04000485 RID: 1157
			AuthzIdentityNotSet = 2642084527U,
			// Token: 0x04000486 RID: 1158
			AuthManagerNotInitialized = 335518370U,
			// Token: 0x04000487 RID: 1159
			ApplicationUriMissing = 3109722150U,
			// Token: 0x04000488 RID: 1160
			CorruptStringSize = 3853787433U,
			// Token: 0x04000489 RID: 1161
			DuplicateItem = 523155784U,
			// Token: 0x0400048A RID: 1162
			BufferMismatch = 3497416644U,
			// Token: 0x0400048B RID: 1163
			CannotHandleUnsecuredRedirection = 282247621U,
			// Token: 0x0400048C RID: 1164
			EndAlreadyCalled = 2788838128U,
			// Token: 0x0400048D RID: 1165
			CollectionChanged = 1998366202U,
			// Token: 0x0400048E RID: 1166
			BadOffset = 3363535402U,
			// Token: 0x0400048F RID: 1167
			AlreadyInitialized = 1479752888U,
			// Token: 0x04000490 RID: 1168
			SignatureDoesNotMatch = 4293010739U,
			// Token: 0x04000491 RID: 1169
			NoTokenContext = 1000630245U,
			// Token: 0x04000492 RID: 1170
			InvalidUnicodeString = 574942079U,
			// Token: 0x04000493 RID: 1171
			DomainsMissing = 2031043979U,
			// Token: 0x04000494 RID: 1172
			OnlySSLSupported = 4205481756U,
			// Token: 0x04000495 RID: 1173
			StoreTypeUnsupported = 1391971810U,
			// Token: 0x04000496 RID: 1174
			MultipleOfAlignmentFactor = 1787132391U,
			// Token: 0x04000497 RID: 1175
			GetUserSettingsGeneralFailure = 3739156314U,
			// Token: 0x04000498 RID: 1176
			NoResponseFromHttpServer = 3669818989U,
			// Token: 0x04000499 RID: 1177
			NoContext = 4039752388U,
			// Token: 0x0400049A RID: 1178
			NegativeIndex = 1520602905U,
			// Token: 0x0400049B RID: 1179
			SmallBuffer = 3658473395U,
			// Token: 0x0400049C RID: 1180
			ReceiveInProgress = 2711616649U,
			// Token: 0x0400049D RID: 1181
			NotInitialized = 994785501U,
			// Token: 0x0400049E RID: 1182
			InvalidDuration = 2115127879U,
			// Token: 0x0400049F RID: 1183
			EmptyCertSubject = 1323030597U,
			// Token: 0x040004A0 RID: 1184
			UnknownAuthMechanism = 3037031059U,
			// Token: 0x040004A1 RID: 1185
			SeekOrigin = 2092010860U,
			// Token: 0x040004A2 RID: 1186
			aadTransportFailureException = 2702236218U,
			// Token: 0x040004A3 RID: 1187
			EmptyFQDNList = 1228304272U,
			// Token: 0x040004A4 RID: 1188
			ResolveInProgress = 2649873638U,
			// Token: 0x040004A5 RID: 1189
			EmptyCertThumbprint = 4160234858U,
			// Token: 0x040004A6 RID: 1190
			AuthzInitializeContextFromDuplicateAuthZFailed = 3484386229U,
			// Token: 0x040004A7 RID: 1191
			StringContainsInvalidCharacters = 1344176929U,
			// Token: 0x040004A8 RID: 1192
			UnexpectedUserResponses = 1018641786U,
			// Token: 0x040004A9 RID: 1193
			LogonData = 2422771817U,
			// Token: 0x040004AA RID: 1194
			AuthFailureException = 1024372311U,
			// Token: 0x040004AB RID: 1195
			ImmutableStream = 2234054046U,
			// Token: 0x040004AC RID: 1196
			NegativeCapacity = 3706950651U,
			// Token: 0x040004AD RID: 1197
			UnsupportedFilter = 3076215569U,
			// Token: 0x040004AE RID: 1198
			InvalidIPType = 1610756644U,
			// Token: 0x040004AF RID: 1199
			TlsProtocolFailureException = 3156409200U,
			// Token: 0x040004B0 RID: 1200
			CorruptArraySize = 1791693013U,
			// Token: 0x040004B1 RID: 1201
			AuthzGetInformationFromContextReturnedSuccessForSize = 1612735551U,
			// Token: 0x040004B2 RID: 1202
			CapacityOverflow = 2043762708U,
			// Token: 0x040004B3 RID: 1203
			ExportAndArchive = 1871965737U,
			// Token: 0x040004B4 RID: 1204
			InternalOperationFailure = 1441393770U,
			// Token: 0x040004B5 RID: 1205
			MissingNullTerminator = 3175999590U,
			// Token: 0x040004B6 RID: 1206
			DSSNotSupported = 2134260535U,
			// Token: 0x040004B7 RID: 1207
			InvalidNumberOfBytes = 2821016632U,
			// Token: 0x040004B8 RID: 1208
			CouldNotAllocateFragment = 2396394977U,
			// Token: 0x040004B9 RID: 1209
			OutOfRange = 565660828U,
			// Token: 0x040004BA RID: 1210
			ResponseMissingErrorCode = 115723314U,
			// Token: 0x040004BB RID: 1211
			IllegalContainedAccess = 1151742595U,
			// Token: 0x040004BC RID: 1212
			UseReportEncryptedBytesFilled = 2305890504U,
			// Token: 0x040004BD RID: 1213
			SendInProgress = 4177743836U,
			// Token: 0x040004BE RID: 1214
			LargeIndex = 976065123U,
			// Token: 0x040004BF RID: 1215
			InvalidSize = 1274073424U,
			// Token: 0x040004C0 RID: 1216
			EmptyResponse = 1896894556U,
			// Token: 0x040004C1 RID: 1217
			CollectionEmpty = 1753773471U,
			// Token: 0x040004C2 RID: 1218
			NegativeParameter = 2455122056U,
			// Token: 0x040004C3 RID: 1219
			MustBeTlsForAuthException = 907185727U,
			// Token: 0x040004C4 RID: 1220
			WrongType = 1927799671U,
			// Token: 0x040004C5 RID: 1221
			DataContainsBareLinefeeds = 1586250006U,
			// Token: 0x040004C6 RID: 1222
			ClosedStream = 1945685814U,
			// Token: 0x040004C7 RID: 1223
			MissingPrimaryGroupSid = 2499062057U,
			// Token: 0x040004C8 RID: 1224
			ReadPastEnd = 1163610383U,
			// Token: 0x040004C9 RID: 1225
			TlsAlreadyNegotiated = 1890952107U,
			// Token: 0x040004CA RID: 1226
			DestinationIndexOutOfRange = 2126643456U,
			// Token: 0x040004CB RID: 1227
			TruncatedData = 3093826520U
		}

		// Token: 0x020000DE RID: 222
		private enum ParamIDs
		{
			// Token: 0x040004CD RID: 1229
			AuthzUnableToGetTokenFromNullOrInvalidHandle,
			// Token: 0x040004CE RID: 1230
			TlsApiFailureException,
			// Token: 0x040004CF RID: 1231
			CertificateSubjectNotFound,
			// Token: 0x040004D0 RID: 1232
			InvalidSmtpServerResponseException,
			// Token: 0x040004D1 RID: 1233
			UnmanagedAlloc,
			// Token: 0x040004D2 RID: 1234
			InvalidWaveFormat,
			// Token: 0x040004D3 RID: 1235
			DomainNotPresentInResponse,
			// Token: 0x040004D4 RID: 1236
			UnexpectedAutodiscoverResult,
			// Token: 0x040004D5 RID: 1237
			AuthzUnableToPerformAccessCheck,
			// Token: 0x040004D6 RID: 1238
			MalformedLocationHeader,
			// Token: 0x040004D7 RID: 1239
			UnexpectedSmtpServerResponseException,
			// Token: 0x040004D8 RID: 1240
			FailedToConnectToSMTPServerException,
			// Token: 0x040004D9 RID: 1241
			LogonAsNetworkServiceFailed,
			// Token: 0x040004DA RID: 1242
			InvalidSid,
			// Token: 0x040004DB RID: 1243
			UnsupportedAudioFormat,
			// Token: 0x040004DC RID: 1244
			AudioConversionFailed,
			// Token: 0x040004DD RID: 1245
			NotConnectedToSMTPServerException,
			// Token: 0x040004DE RID: 1246
			AuthzInitializeContextFromTokenFailed,
			// Token: 0x040004DF RID: 1247
			InvalidFlags,
			// Token: 0x040004E0 RID: 1248
			AuthzGetInformationFromContextFailed,
			// Token: 0x040004E1 RID: 1249
			AuthApiFailureException,
			// Token: 0x040004E2 RID: 1250
			AuthzInitializeContextFromSidFailed,
			// Token: 0x040004E3 RID: 1251
			UnexpectedStatusCode,
			// Token: 0x040004E4 RID: 1252
			UnexpectedResponseType,
			// Token: 0x040004E5 RID: 1253
			CertificateThumbprintNotFound,
			// Token: 0x040004E6 RID: 1254
			DiscoveryFailed,
			// Token: 0x040004E7 RID: 1255
			UnsupportedAuthMechanismException,
			// Token: 0x040004E8 RID: 1256
			InvalidUserForGetUserSettings,
			// Token: 0x040004E9 RID: 1257
			InvalidFQDN,
			// Token: 0x040004EA RID: 1258
			CertificateIssuerNotfound,
			// Token: 0x040004EB RID: 1259
			AlreadyConnectedToSMTPServerException,
			// Token: 0x040004EC RID: 1260
			ErrorInResponse,
			// Token: 0x040004ED RID: 1261
			WildcardNotSupported,
			// Token: 0x040004EE RID: 1262
			NonHttpsLocationHeader,
			// Token: 0x040004EF RID: 1263
			UnexpectedPathLocationHeader,
			// Token: 0x040004F0 RID: 1264
			ApplicationUriMalformed,
			// Token: 0x040004F1 RID: 1265
			AuthzAddSidsToContextFailed
		}
	}
}
