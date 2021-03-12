using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000924 RID: 2340
	internal class PowershellHostUI : PSHostUserInterface
	{
		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x00157D8C File Offset: 0x00155F8C
		public override PSHostRawUserInterface RawUI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00157D8F File Offset: 0x00155F8F
		public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x00157D9B File Offset: 0x00155F9B
		public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00157DA7 File Offset: 0x00155FA7
		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x00157DB3 File Offset: 0x00155FB3
		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x00157DBF File Offset: 0x00155FBF
		public override string ReadLine()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00157DCB File Offset: 0x00155FCB
		public override SecureString ReadLineAsSecureString()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x00157DD7 File Offset: 0x00155FD7
		public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
		{
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x00157DD9 File Offset: 0x00155FD9
		public override void Write(string value)
		{
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x00157DDB File Offset: 0x00155FDB
		public override void WriteDebugLine(string message)
		{
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00157DDD File Offset: 0x00155FDD
		public override void WriteLine(string value)
		{
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00157DDF File Offset: 0x00155FDF
		public override void WriteVerboseLine(string message)
		{
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x00157DE1 File Offset: 0x00155FE1
		public override void WriteProgress(long value, ProgressRecord record)
		{
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x00157DE3 File Offset: 0x00155FE3
		public override void WriteErrorLine(string message)
		{
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x00157DE5 File Offset: 0x00155FE5
		public override void WriteWarningLine(string message)
		{
		}
	}
}
