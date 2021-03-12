using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000035 RID: 53
	internal class PowershellHostUI : PSHostUserInterface
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F7E File Offset: 0x0000317E
		public override PSHostRawUserInterface RawUI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004F81 File Offset: 0x00003181
		public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004F8D File Offset: 0x0000318D
		public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004F99 File Offset: 0x00003199
		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004FA5 File Offset: 0x000031A5
		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004FB1 File Offset: 0x000031B1
		public override string ReadLine()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004FBD File Offset: 0x000031BD
		public override SecureString ReadLineAsSecureString()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004FC9 File Offset: 0x000031C9
		public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004FCB File Offset: 0x000031CB
		public override void Write(string value)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004FCD File Offset: 0x000031CD
		public override void WriteDebugLine(string message)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004FCF File Offset: 0x000031CF
		public override void WriteLine(string value)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004FD1 File Offset: 0x000031D1
		public override void WriteVerboseLine(string message)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004FD3 File Offset: 0x000031D3
		public override void WriteProgress(long value, ProgressRecord record)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004FD5 File Offset: 0x000031D5
		public override void WriteErrorLine(string message)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004FD7 File Offset: 0x000031D7
		public override void WriteWarningLine(string message)
		{
		}
	}
}
