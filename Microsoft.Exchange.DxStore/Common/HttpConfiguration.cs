using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000070 RID: 112
	public static class HttpConfiguration
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000102F3 File Offset: 0x0000E4F3
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000102FA File Offset: 0x0000E4FA
		public static bool UseEncryption { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00010302 File Offset: 0x0000E502
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00010309 File Offset: 0x0000E509
		public static bool IsZeroboxMode { get; set; }

		// Token: 0x060004BA RID: 1210 RVA: 0x00010314 File Offset: 0x0000E514
		public static void Configure(InstanceGroupConfig groupConfig)
		{
			if (!HttpConfiguration.isConfigured)
			{
				lock (HttpConfiguration.lockObj)
				{
					if (!HttpConfiguration.isConfigured)
					{
						DxSerializationUtil.UseBinarySerialize = groupConfig.Settings.IsUseBinarySerializerForClientCommunication;
						HttpConfiguration.UseEncryption = groupConfig.Settings.IsUseEncryption;
						HttpConfiguration.IsZeroboxMode = groupConfig.IsZeroboxMode;
						bool flag2 = true;
						if (HttpConfiguration.UseEncryption && flag2)
						{
							ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpConfiguration.IgnoreCertValidate);
						}
						else
						{
							ServicePointManager.ServerCertificateValidationCallback = null;
						}
						HttpConfiguration.isConfigured = true;
					}
				}
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public static string FormClientUriPrefix(string targetServerAddr, string targetNodeId, string groupName)
		{
			if (HttpConfiguration.IsZeroboxMode)
			{
				targetServerAddr = "localhost";
				return string.Format("http{0}://{1}/{2}/{3}/{4}/", new object[]
				{
					HttpConfiguration.UseEncryption ? "s" : string.Empty,
					targetServerAddr,
					"Microsoft.Exchange.DxStore.Http",
					groupName,
					targetNodeId
				});
			}
			return string.Format("http{0}://{1}/{2}/{3}/", new object[]
			{
				HttpConfiguration.UseEncryption ? "s" : string.Empty,
				targetServerAddr,
				"Microsoft.Exchange.DxStore.Http",
				groupName
			});
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00010448 File Offset: 0x0000E648
		public static string FormServerUriPrefix(string self, string groupName)
		{
			string text = "+";
			if (HttpConfiguration.IsZeroboxMode)
			{
				return string.Format("http{0}://{1}/{2}/{3}/{4}/", new object[]
				{
					HttpConfiguration.UseEncryption ? "s" : string.Empty,
					text,
					"Microsoft.Exchange.DxStore.Http",
					groupName,
					self
				});
			}
			return string.Format("http{0}://{1}/{2}/{3}/", new object[]
			{
				HttpConfiguration.UseEncryption ? "s" : string.Empty,
				text,
				"Microsoft.Exchange.DxStore.Http",
				groupName
			});
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000104DC File Offset: 0x0000E6DC
		private static bool IgnoreCertValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
		{
			bool flag = false;
			if (!HttpConfiguration.invalidCertLogged)
			{
				lock (HttpConfiguration.lockObj)
				{
					if (!HttpConfiguration.invalidCertLogged)
					{
						flag = true;
						HttpConfiguration.invalidCertLogged = true;
					}
				}
			}
			if (flag)
			{
				EventLogger.LogErr("IgnoreCertValidate ignored {0}", new object[]
				{
					cert
				});
			}
			return true;
		}

		// Token: 0x0400022C RID: 556
		public const string UriNameSpace = "Microsoft.Exchange.DxStore.Http";

		// Token: 0x0400022D RID: 557
		private static bool isConfigured;

		// Token: 0x0400022E RID: 558
		private static object lockObj = new object();

		// Token: 0x0400022F RID: 559
		private static bool invalidCertLogged = false;
	}
}
