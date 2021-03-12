using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000AE RID: 174
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMSubscriberCallAnsweringDataResponseMessageType : ResponseMessageType
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001FC27 File Offset: 0x0001DE27
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0001FC2F File Offset: 0x0001DE2F
		public bool IsOOF
		{
			get
			{
				return this.isOOFField;
			}
			set
			{
				this.isOOFField = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001FC38 File Offset: 0x0001DE38
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0001FC40 File Offset: 0x0001DE40
		public UMMailboxTranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig
		{
			get
			{
				return this.isTranscriptionEnabledInMailboxConfigField;
			}
			set
			{
				this.isTranscriptionEnabledInMailboxConfigField = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001FC49 File Offset: 0x0001DE49
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0001FC51 File Offset: 0x0001DE51
		public bool IsMailboxQuotaExceeded
		{
			get
			{
				return this.isMailboxQuotaExceededField;
			}
			set
			{
				this.isMailboxQuotaExceededField = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001FC62 File Offset: 0x0001DE62
		[XmlElement(DataType = "base64Binary")]
		public byte[] Greeting
		{
			get
			{
				return this.greetingField;
			}
			set
			{
				this.greetingField = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001FC6B File Offset: 0x0001DE6B
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001FC73 File Offset: 0x0001DE73
		public string GreetingName
		{
			get
			{
				return this.greetingNameField;
			}
			set
			{
				this.greetingNameField = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0001FC84 File Offset: 0x0001DE84
		public bool TaskTimedOut
		{
			get
			{
				return this.taskTimedOutField;
			}
			set
			{
				this.taskTimedOutField = value;
			}
		}

		// Token: 0x0400053F RID: 1343
		private bool isOOFField;

		// Token: 0x04000540 RID: 1344
		private UMMailboxTranscriptionEnabledSetting isTranscriptionEnabledInMailboxConfigField;

		// Token: 0x04000541 RID: 1345
		private bool isMailboxQuotaExceededField;

		// Token: 0x04000542 RID: 1346
		private byte[] greetingField;

		// Token: 0x04000543 RID: 1347
		private string greetingNameField;

		// Token: 0x04000544 RID: 1348
		private bool taskTimedOutField;
	}
}
