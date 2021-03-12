using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000757 RID: 1879
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInRegistryReader
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
		private LinkedInRegistryReader()
		{
			this.AppId = string.Empty;
			this.AppSecret = string.Empty;
			this.AccessTokenEndpoint = string.Empty;
			this.ConnectionsEndpoint = string.Empty;
			this.ProfileEndpoint = string.Empty;
			this.RemoveAppEndpoint = string.Empty;
			this.RequestTokenEndpoint = string.Empty;
			this.WebRequestTimeout = TimeSpan.Zero;
			this.WebProxyUri = string.Empty;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0004CD60 File Offset: 0x0004AF60
		public static LinkedInRegistryReader Read()
		{
			LinkedInRegistryReader result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PeopleConnect\\LinkedIn"))
				{
					if (registryKey == null)
					{
						result = new LinkedInRegistryReader();
					}
					else
					{
						result = new LinkedInRegistryReader
						{
							AppId = (string)registryKey.GetValue("AppId", string.Empty),
							AppSecret = (string)registryKey.GetValue("AppSecret", string.Empty),
							RequestTokenEndpoint = (string)registryKey.GetValue("RequestTokenEndpoint", string.Empty),
							AccessTokenEndpoint = (string)registryKey.GetValue("AccessTokenEndpoint", string.Empty),
							ConnectionsEndpoint = (string)registryKey.GetValue("ConnectionsEndpoint", string.Empty),
							ProfileEndpoint = (string)registryKey.GetValue("ProfileEndpoint", string.Empty),
							RemoveAppEndpoint = (string)registryKey.GetValue("RemoveAppEndpoint", string.Empty),
							ConsentRedirectEndpoint = (string)registryKey.GetValue("ConsentRedirectEndpoint", string.Empty),
							WebRequestTimeout = TimeSpan.FromSeconds((double)((int)registryKey.GetValue("WebRequestTimeoutSeconds", 0))),
							WebProxyUri = (string)registryKey.GetValue("WebProxyUri", string.Empty)
						};
					}
				}
			}
			catch (SecurityException e)
			{
				result = LinkedInRegistryReader.TraceErrorAndReturnEmptyConfiguration(e);
			}
			catch (IOException e2)
			{
				result = LinkedInRegistryReader.TraceErrorAndReturnEmptyConfiguration(e2);
			}
			catch (UnauthorizedAccessException e3)
			{
				result = LinkedInRegistryReader.TraceErrorAndReturnEmptyConfiguration(e3);
			}
			return result;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0004CF40 File Offset: 0x0004B140
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x0004CF48 File Offset: 0x0004B148
		public string AppId { get; private set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0004CF51 File Offset: 0x0004B151
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x0004CF59 File Offset: 0x0004B159
		public string AppSecret { get; private set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0004CF62 File Offset: 0x0004B162
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x0004CF6A File Offset: 0x0004B16A
		public string RequestTokenEndpoint { get; private set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0004CF73 File Offset: 0x0004B173
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x0004CF7B File Offset: 0x0004B17B
		public string AccessTokenEndpoint { get; private set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0004CF84 File Offset: 0x0004B184
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x0004CF8C File Offset: 0x0004B18C
		public string ConnectionsEndpoint { get; private set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0004CF95 File Offset: 0x0004B195
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0004CF9D File Offset: 0x0004B19D
		public string ProfileEndpoint { get; private set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x0004CFA6 File Offset: 0x0004B1A6
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x0004CFAE File Offset: 0x0004B1AE
		public string RemoveAppEndpoint { get; private set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x0004CFB7 File Offset: 0x0004B1B7
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x0004CFBF File Offset: 0x0004B1BF
		public string ConsentRedirectEndpoint { get; private set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0004CFC8 File Offset: 0x0004B1C8
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x0004CFD0 File Offset: 0x0004B1D0
		public TimeSpan WebRequestTimeout { get; private set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x0004CFE1 File Offset: 0x0004B1E1
		public string WebProxyUri { get; private set; }

		// Token: 0x060024DE RID: 9438 RVA: 0x0004CFEA File Offset: 0x0004B1EA
		private static LinkedInRegistryReader TraceErrorAndReturnEmptyConfiguration(Exception e)
		{
			LinkedInRegistryReader.Tracer.TraceError<Exception>(0L, "LinkedInRegistryReader.Read: caught exception {0}", e);
			return new LinkedInRegistryReader();
		}

		// Token: 0x04002260 RID: 8800
		private const string AppIdValueKey = "AppId";

		// Token: 0x04002261 RID: 8801
		private const string AppSecretKey = "AppSecret";

		// Token: 0x04002262 RID: 8802
		private const string RequestTokenEndpointKey = "RequestTokenEndpoint";

		// Token: 0x04002263 RID: 8803
		private const string AccessTokenEndpointKey = "AccessTokenEndpoint";

		// Token: 0x04002264 RID: 8804
		private const string ConnectionsEndpointKey = "ConnectionsEndpoint";

		// Token: 0x04002265 RID: 8805
		private const string ProfileEndpointKey = "ProfileEndpoint";

		// Token: 0x04002266 RID: 8806
		private const string RemoveAppEndpointKey = "RemoveAppEndpoint";

		// Token: 0x04002267 RID: 8807
		private const string ConsentRedirectEndpointValueName = "ConsentRedirectEndpoint";

		// Token: 0x04002268 RID: 8808
		private const string WebRequestTimeoutSecondsKey = "WebRequestTimeoutSeconds";

		// Token: 0x04002269 RID: 8809
		private const string WebProxyUriKey = "WebProxyUri";

		// Token: 0x0400226A RID: 8810
		private const string LinkedInKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PeopleConnect\\LinkedIn";

		// Token: 0x0400226B RID: 8811
		private static readonly Trace Tracer = ExTraceGlobals.LinkedInTracer;
	}
}
