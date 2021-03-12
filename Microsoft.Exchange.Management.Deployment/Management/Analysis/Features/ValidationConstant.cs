using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000067 RID: 103
	public static class ValidationConstant
	{
		// Token: 0x04000167 RID: 359
		public const string DNSPort = "53";

		// Token: 0x04000168 RID: 360
		public const string PrimaryDNSPortString = "PrimaryDNSPort";

		// Token: 0x04000169 RID: 361
		public const string Exchange2000SerialNumber = "Version 6.0";

		// Token: 0x0400016A RID: 362
		public const string Exchange2003SerialNumber = "Version 6.5";

		// Token: 0x0400016B RID: 363
		public const string Exchange200xSerialNumber = "Version 6";

		// Token: 0x0400016C RID: 364
		public const string ExchangeProductName = "Exchange 15";

		// Token: 0x0400016D RID: 365
		public const string FileInUseProcessExclusionListPattern = "^(Setup|Exsetup|ExSetupUI|WmiPrvSE|MOM|MonitoringHost|w3wp|msftesql|msftefd|EdgeTransport|mad|store|umservice|UMWorkerProcess|TranscodingService|SESWorker|ExBPA|ExFBA|wsbexchange|hostcontrollerservice|noderunner|parserserver|Microsoft\\.Exchange\\..*|MSExchange.*|fms|scanningprocess|FSCConfigurationServer|updateservice|ScanEngineTest|EngineUpdateLogger|sftracing|ForefrontActiveDirectoryConnector|rundll32|MSMessageTracingClient)$";

		// Token: 0x0400016E RID: 366
		public const string DatacenterFileInUseProcessExclusionListPattern = "^(Setup|Exsetup|ExSetupUI|WmiPrvSE|MOM|MonitoringHost|w3wp|msftesql|msftefd|EdgeTransport|mad|store|umservice|UMWorkerProcess|TranscodingService|SESWorker|ExBPA|ExFBA|wsbexchange|hostcontrollerservice|noderunner|parserserver|Microsoft\\.Exchange\\..*|MSExchange.*|fms|scanningprocess|FSCConfigurationServer|updateservice|ScanEngineTest|EngineUpdateLogger|sftracing|ForefrontActiveDirectoryConnector|rundll32|MSMessageTracingClient|wsmprovhost|Microsoft.Office.BigData.DataLoader)$";

		// Token: 0x0400016F RID: 367
		public const string FileVersionPattern = "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$";

		// Token: 0x04000170 RID: 368
		public const string IISVersion = "IIS 6";

		// Token: 0x04000171 RID: 369
		public const string IPAddressPattern = "^(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}).*$";

		// Token: 0x04000172 RID: 370
		public const string IPAddressMatch = "^\\[\\d+\\.\\d+\\.\\d+\\.\\d+\\]$";

		// Token: 0x04000173 RID: 371
		public const string LocalAdminSid = "S-1-5-32-544";

		// Token: 0x04000174 RID: 372
		public const string SchemaAdminSid = "S-1-5-21-1413246421-4226699797-92697236-518";

		// Token: 0x04000175 RID: 373
		public const string EnterpriseAdminSid = "S-1-5-21-1413246421-4226699797-92697236-519";

		// Token: 0x04000176 RID: 374
		public const int MinExchangeBuild = 10000;

		// Token: 0x04000177 RID: 375
		public const int MinSchemaVersionRangeUpper = 14622;

		// Token: 0x04000178 RID: 376
		public const string RegistryExchange2010Path = "SOFTWARE\\Microsoft\\ExchangeServer\\v14";

		// Token: 0x04000179 RID: 377
		public const string RegistryExchange2007Path = "SOFTWARE\\Microsoft\\Exchange\\v8.0";

		// Token: 0x0400017A RID: 378
		public const string HubTransportRoleName = "HubTransportRole";

		// Token: 0x0400017B RID: 379
		public const string ClientAccessRoleName = "ClientAccessRole";

		// Token: 0x0400017C RID: 380
		public const string EdgeRoleName = "Hygiene";

		// Token: 0x0400017D RID: 381
		public const string MailboxRoleName = "MailboxRole";

		// Token: 0x0400017E RID: 382
		public const string UnifiedMessagingRoleName = "UnifiedMessagingRole";

		// Token: 0x0400017F RID: 383
		public const string AdminToolsRoleName = "AdminTools";

		// Token: 0x04000180 RID: 384
		public const string RegistryExchangeSetupPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000181 RID: 385
		public const string RegistryMicrosoftInetStp = "Software\\Microsoft\\InetStp";

		// Token: 0x04000182 RID: 386
		public const string RegistryMicrosoftInetStpComponents = "Software\\Microsoft\\InetStp\\Components";

		// Token: 0x04000183 RID: 387
		public const string RegistryMicrosoftWindowsNTCurrentVersion = "Software\\Microsoft\\Windows NT\\CurrentVersion";

		// Token: 0x04000184 RID: 388
		public const string RegistryUCMA = "SOFTWARE\\Microsoft\\UCMA\\{902F4F35-D5DC-4363-8671-D5EF0D26C21D}";

		// Token: 0x04000185 RID: 389
		public const string RegistryMicrosoftWindowsCurrentVersion = "Software\\Microsoft\\Windows\\CurrentVersion";

		// Token: 0x04000186 RID: 390
		public const string RegistrySystemCurrentControlSetServices = "System\\CurrentControlSet\\Services";

		// Token: 0x04000187 RID: 391
		public const string RoleMatch = "(.*Role)";

		// Token: 0x04000188 RID: 392
		public const string RoleFilterPattern = "^.*\\\\(.*)Role$";

		// Token: 0x04000189 RID: 393
		public const string SmtpAddressPattern = "^((?i:smtp)\\:.*\\@(?'smtpaddress'.*))?.*$";

		// Token: 0x0400018A RID: 394
		public const string SmtpAddressReplacement = "${smtpaddress}";

		// Token: 0x0400018B RID: 395
		public const string SmtpDomainPattern = "^smtp:.*\\@(?'domain'.*))?.*$";

		// Token: 0x0400018C RID: 396
		public const string DomainReplacement = "${domain}";

		// Token: 0x0400018D RID: 397
		public const string SystemDrive = "%systemdrive%";

		// Token: 0x0400018E RID: 398
		public const int WindowsMajorVersion = 6;

		// Token: 0x0400018F RID: 399
		public const int WindowsMinorVersion = 1;

		// Token: 0x04000190 RID: 400
		public const int WindowsServicePack = 1;

		// Token: 0x04000191 RID: 401
		public const int Windows8MinorVerison = 2;

		// Token: 0x04000192 RID: 402
		public const string PrepareDomainGuid = "F63C3A12-7852-4654-B208-125C32EB409A";

		// Token: 0x04000193 RID: 403
		public const string PrepareLegacyExchangePermissionsGuid = "2A7F95FC-66C6-445F-AAB9-19744C05E70E";

		// Token: 0x04000194 RID: 404
		public const int AdjustMajorBuild = 10000;

		// Token: 0x04000195 RID: 405
		public const string ObjectTypeListString = "'0;a8df74a7-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74b2-c5ea-11d1-bbcb-0080c76670c0'|'0;bf967a8b-0de6-11d0-a285-00aa003049e2'|'0;28630ec1-41d5-11d1-a9c1-0000f80367c1'|'0;031b371a-a981-11d2-a9ff-00c04f8eedd8'|'0;3435244a-a982-11d2-a9ff-00c04f8eedd8'|'0;36145cf4-a982-11d2-a9ff-00c04f8eedd8'|'0;966540a1-75f7-4d27-ace9-3858b5dea688'|'0;9432cae6-b09e-11d2-aa06-00c04f8eedd8'|'0;93da93e4-b09e-11d2-aa06-00c04f8eedd8'|'0;a8df74d1-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74c5-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74ce-c5ea-11d1-bbcb-0080c76670c0'|'0;3378ca84-a982-11d2-a9ff-00c04f8eedd8'|'0;33bb8c5c-a982-11d2-a9ff-00c04f8eedd8'|'0;3397c916-a982-11d2-a9ff-00c04f8eedd8'|'0;8ef628c6-b093-11d2-aa06-00c04f8eedd8'|'0;8ef628c6-b093-11d2-aa06-00c04f8eedd8'|'0;93bb9552-b09e-11d2-aa06-00c04f8eedd8'|'0;44601346-776a-46e7-b4a4-2472e1c66806'|'0;20309cbd-0ae3-4876-9114-5738c65f845c'";

		// Token: 0x04000196 RID: 406
		public const string RegistryExchangePath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000197 RID: 407
		public const string MinVersionAtl110VC2012 = "11.00.50727.1";

		// Token: 0x04000198 RID: 408
		public const string MinVersionMsvcr120VC2013 = "12.00.21005.1";

		// Token: 0x04000199 RID: 409
		public static readonly Version AllServersOfHigherVersionMinimum = new Version("15.1");

		// Token: 0x0400019A RID: 410
		public static readonly Version E12MinCoExistVersionNumber = new Version("8.3.83.0");

		// Token: 0x0400019B RID: 411
		public static readonly Version E14MinCoExistVersionNumber = new Version("14.3.71.0");

		// Token: 0x0400019C RID: 412
		public static readonly Version E14MinCoExistMajorVersionNumber = new Version("14.3.0.0");

		// Token: 0x02000068 RID: 104
		public enum DomainRole : ushort
		{
			// Token: 0x0400019E RID: 414
			StandaloneWorkstation,
			// Token: 0x0400019F RID: 415
			MemberWorkstation,
			// Token: 0x040001A0 RID: 416
			StandaloneServer,
			// Token: 0x040001A1 RID: 417
			MemberServer,
			// Token: 0x040001A2 RID: 418
			BackupDomainController,
			// Token: 0x040001A3 RID: 419
			PrimaryDomainController,
			// Token: 0x040001A4 RID: 420
			None = 10
		}

		// Token: 0x02000069 RID: 105
		public enum ComputerNameFormat
		{
			// Token: 0x040001A6 RID: 422
			ComputerNameNetBIOS,
			// Token: 0x040001A7 RID: 423
			ComputerNameDnsHostname,
			// Token: 0x040001A8 RID: 424
			ComputerNameDnsDomain,
			// Token: 0x040001A9 RID: 425
			ComputerNameDnsFullyQualified,
			// Token: 0x040001AA RID: 426
			ComputerNamePhysicalNetBIOS,
			// Token: 0x040001AB RID: 427
			ComputerNamePhysicalDnsHostname,
			// Token: 0x040001AC RID: 428
			ComputerNamePhysicalDnsDomain,
			// Token: 0x040001AD RID: 429
			ComputerNamePhysicalDnsFullyQualified,
			// Token: 0x040001AE RID: 430
			ComputerNameMax
		}

		// Token: 0x0200006A RID: 106
		public enum ExtendedNameFormat
		{
			// Token: 0x040001B0 RID: 432
			NameUnknown,
			// Token: 0x040001B1 RID: 433
			NameFullyQualifiedDN,
			// Token: 0x040001B2 RID: 434
			NameSamCompatible,
			// Token: 0x040001B3 RID: 435
			NameDisplay,
			// Token: 0x040001B4 RID: 436
			NameUniqueId = 6,
			// Token: 0x040001B5 RID: 437
			NameCanonical,
			// Token: 0x040001B6 RID: 438
			NameUserPrinciple,
			// Token: 0x040001B7 RID: 439
			NameCanonicalEx,
			// Token: 0x040001B8 RID: 440
			NameServicePrinciple,
			// Token: 0x040001B9 RID: 441
			NameDnsDomain = 12
		}

		// Token: 0x0200006B RID: 107
		[Flags]
		public enum SecurityDescriptorControl : ushort
		{
			// Token: 0x040001BB RID: 443
			SE_OWNER_DEFAULTED = 1,
			// Token: 0x040001BC RID: 444
			SE_GROUP_DEFAULTED = 2,
			// Token: 0x040001BD RID: 445
			SE_DACL_PRESENT = 4,
			// Token: 0x040001BE RID: 446
			SE_DACL_DEFAULTED = 8,
			// Token: 0x040001BF RID: 447
			SE_SACL_PRESENT = 16,
			// Token: 0x040001C0 RID: 448
			SE_SACL_DEFAULTED = 32,
			// Token: 0x040001C1 RID: 449
			SE_DACL_UNTRUSTED = 64,
			// Token: 0x040001C2 RID: 450
			SE_SERVER_SECURITY = 128,
			// Token: 0x040001C3 RID: 451
			SE_DACL_AUTO_INHERIT_REQ = 256,
			// Token: 0x040001C4 RID: 452
			SE_SACL_AUTO_INHERIT_REQ = 512,
			// Token: 0x040001C5 RID: 453
			SE_DACL_AUTO_INHERITED = 1024,
			// Token: 0x040001C6 RID: 454
			SE_SACL_AUTO_INHERITED = 2048,
			// Token: 0x040001C7 RID: 455
			SE_DACL_PROTECTED = 4096,
			// Token: 0x040001C8 RID: 456
			SE_SACL_PROTECTED = 8192,
			// Token: 0x040001C9 RID: 457
			SE_RM_CONTROL_VALID = 16384,
			// Token: 0x040001CA RID: 458
			SE_SELF_RELATIVE = 32768
		}
	}
}
