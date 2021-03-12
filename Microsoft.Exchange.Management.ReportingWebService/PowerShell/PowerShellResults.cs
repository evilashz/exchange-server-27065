using System;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x0200000F RID: 15
	internal class PowerShellResults
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002B28 File Offset: 0x00000D28
		public PowerShellResults() : this(null, null)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B32 File Offset: 0x00000D32
		public PowerShellResults(Collection<PSObject> output) : this(output, null)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B3C File Offset: 0x00000D3C
		public PowerShellResults(Collection<ErrorRecord> errors) : this(null, errors)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B46 File Offset: 0x00000D46
		public PowerShellResults(Collection<PSObject> output, Collection<ErrorRecord> errors)
		{
			this.Output = output;
			this.Errors = errors;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B5C File Offset: 0x00000D5C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002B64 File Offset: 0x00000D64
		public Collection<PSObject> Output { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B6D File Offset: 0x00000D6D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002B75 File Offset: 0x00000D75
		public Collection<ErrorRecord> Errors { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002B7E File Offset: 0x00000D7E
		public bool Succeeded
		{
			get
			{
				return this.Errors == null || this.Errors.Count == 0;
			}
		}
	}
}
