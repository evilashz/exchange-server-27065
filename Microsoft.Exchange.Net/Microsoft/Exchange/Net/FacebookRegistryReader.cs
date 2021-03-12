using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000725 RID: 1829
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FacebookRegistryReader
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x000474B4 File Offset: 0x000456B4
		private FacebookRegistryReader()
		{
			this.AppId = string.Empty;
			this.AppSecret = string.Empty;
			this.AuthorizationEndpoint = string.Empty;
			this.GraphTokenEndpoint = string.Empty;
			this.WebRequestTimeout = TimeSpan.Zero;
			this.SkipContactUpload = false;
			this.ContinueOnContactUploadFailure = true;
			this.WaitForContactUploadCommit = false;
			this.NotifyOnEachContactUpload = false;
			this.MaximumContactsToUpload = 1000;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00047528 File Offset: 0x00045728
		public static FacebookRegistryReader Read()
		{
			FacebookRegistryReader result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(FacebookRegistryReader.FacebookKey))
				{
					if (registryKey == null)
					{
						result = new FacebookRegistryReader();
					}
					else
					{
						result = new FacebookRegistryReader
						{
							AppId = (string)registryKey.GetValue("AppId", string.Empty),
							AppSecret = (string)registryKey.GetValue("AppSecret", string.Empty),
							AuthorizationEndpoint = (string)registryKey.GetValue("AuthorizationEndpoint", string.Empty),
							ConsentRedirectEndpoint = (string)registryKey.GetValue("ConsentRedirectEndpoint", string.Empty),
							GraphTokenEndpoint = (string)registryKey.GetValue("GraphTokenEndpoint", string.Empty),
							GraphApiEndpoint = (string)registryKey.GetValue("GraphApiEndpoint", string.Empty),
							WebRequestTimeout = TimeSpan.FromSeconds((double)((int)registryKey.GetValue("WebRequestTimeoutSeconds", 0))),
							SkipContactUpload = Convert.ToBoolean(registryKey.GetValue("SkipContactUpload", false)),
							ContinueOnContactUploadFailure = Convert.ToBoolean(registryKey.GetValue("ContinueOnContactUploadFailure", true)),
							WaitForContactUploadCommit = Convert.ToBoolean(registryKey.GetValue("WaitForContactUploadCommit", false)),
							NotifyOnEachContactUpload = Convert.ToBoolean(registryKey.GetValue("NotifyOnEachContactUpload", false)),
							MaximumContactsToUpload = (int)registryKey.GetValue("MaximumContactsToUpload", 1000)
						};
					}
				}
			}
			catch (SecurityException e)
			{
				result = FacebookRegistryReader.TraceErrorAndReturnEmptyConfiguration(e);
			}
			catch (IOException e2)
			{
				result = FacebookRegistryReader.TraceErrorAndReturnEmptyConfiguration(e2);
			}
			catch (UnauthorizedAccessException e3)
			{
				result = FacebookRegistryReader.TraceErrorAndReturnEmptyConfiguration(e3);
			}
			return result;
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00047748 File Offset: 0x00045948
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x00047750 File Offset: 0x00045950
		public string AppId { get; private set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00047759 File Offset: 0x00045959
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x00047761 File Offset: 0x00045961
		public string AppSecret { get; private set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x0004776A File Offset: 0x0004596A
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x00047772 File Offset: 0x00045972
		public string AuthorizationEndpoint { get; private set; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0004777B File Offset: 0x0004597B
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x00047783 File Offset: 0x00045983
		public string GraphTokenEndpoint { get; private set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0004778C File Offset: 0x0004598C
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x00047794 File Offset: 0x00045994
		public string GraphApiEndpoint { get; private set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0004779D File Offset: 0x0004599D
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x000477A5 File Offset: 0x000459A5
		public string ConsentRedirectEndpoint { get; private set; }

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000477AE File Offset: 0x000459AE
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x000477B6 File Offset: 0x000459B6
		public TimeSpan WebRequestTimeout { get; private set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000477BF File Offset: 0x000459BF
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x000477C7 File Offset: 0x000459C7
		public bool SkipContactUpload { get; private set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x000477D0 File Offset: 0x000459D0
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x000477D8 File Offset: 0x000459D8
		public bool ContinueOnContactUploadFailure { get; private set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000477E1 File Offset: 0x000459E1
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x000477E9 File Offset: 0x000459E9
		public bool WaitForContactUploadCommit { get; private set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000477F2 File Offset: 0x000459F2
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x000477FA File Offset: 0x000459FA
		public bool NotifyOnEachContactUpload { get; private set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00047803 File Offset: 0x00045A03
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x0004780B File Offset: 0x00045A0B
		public int MaximumContactsToUpload { get; private set; }

		// Token: 0x060022CF RID: 8911 RVA: 0x00047814 File Offset: 0x00045A14
		private static FacebookRegistryReader TraceErrorAndReturnEmptyConfiguration(Exception e)
		{
			FacebookRegistryReader.Tracer.TraceError<Exception>(0L, "FacebookRegistryReader.Read: caught exception {0}", e);
			return new FacebookRegistryReader();
		}

		// Token: 0x040020EF RID: 8431
		private const string AppIdValueName = "AppId";

		// Token: 0x040020F0 RID: 8432
		private const string AppSecretValueName = "AppSecret";

		// Token: 0x040020F1 RID: 8433
		private const string AuthorizationEndpointName = "AuthorizationEndpoint";

		// Token: 0x040020F2 RID: 8434
		private const string GraphTokenEndpointValueName = "GraphTokenEndpoint";

		// Token: 0x040020F3 RID: 8435
		private const string GraphApiEndpointValueName = "GraphApiEndpoint";

		// Token: 0x040020F4 RID: 8436
		private const string ConsentRedirectEndpointValueName = "ConsentRedirectEndpoint";

		// Token: 0x040020F5 RID: 8437
		private const string WebRequestTimeoutSecondsValueName = "WebRequestTimeoutSeconds";

		// Token: 0x040020F6 RID: 8438
		private const string SkipContactUploadName = "SkipContactUpload";

		// Token: 0x040020F7 RID: 8439
		private const string ContinueOnContactUploadFailureName = "ContinueOnContactUploadFailure";

		// Token: 0x040020F8 RID: 8440
		private const string WaitForContactUploadCommitName = "WaitForContactUploadCommit";

		// Token: 0x040020F9 RID: 8441
		private const string NotifyOnEachContactUploadName = "NotifyOnEachContactUpload";

		// Token: 0x040020FA RID: 8442
		private const string MaximumContactsToUploadName = "MaximumContactsToUpload";

		// Token: 0x040020FB RID: 8443
		private const bool SkipContactUploadDefaultValue = false;

		// Token: 0x040020FC RID: 8444
		private const bool ContinueOnContactUploadFailureDefaultValue = true;

		// Token: 0x040020FD RID: 8445
		private const bool WaitForContactUploadCommitDefaultValue = false;

		// Token: 0x040020FE RID: 8446
		private const bool NotifyOnEachContactUploadDefaultValue = false;

		// Token: 0x040020FF RID: 8447
		private const int MaximumContactsToUploadDefaultValue = 1000;

		// Token: 0x04002100 RID: 8448
		private static readonly Trace Tracer = ExTraceGlobals.FacebookTracer;

		// Token: 0x04002101 RID: 8449
		private static readonly string FacebookKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PeopleConnect\\Facebook";
	}
}
