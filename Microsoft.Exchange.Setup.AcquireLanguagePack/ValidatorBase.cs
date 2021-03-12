using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000016 RID: 22
	internal abstract class ValidatorBase : IDisposable
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003401 File Offset: 0x00001601
		protected ValidatorBase()
		{
			this.validatedFiles = new List<string>();
			this.disposed = false;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000341B File Offset: 0x0000161B
		public List<string> ValidatedFiles
		{
			get
			{
				return this.validatedFiles;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003423 File Offset: 0x00001623
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000342B File Offset: 0x0000162B
		protected Action<object> Callback { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00003434 File Offset: 0x00001634
		protected void InvokeCallback(object obj)
		{
			if (this.Callback != null && obj != null)
			{
				this.Callback(obj);
			}
		}

		// Token: 0x06000063 RID: 99
		public abstract bool Validate();

		// Token: 0x06000064 RID: 100 RVA: 0x0000344D File Offset: 0x0000164D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000345C File Offset: 0x0000165C
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && Directory.Exists(ValidatorBase.TempPath))
				{
					Directory.Delete(ValidatorBase.TempPath, true);
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000038 RID: 56
		internal static readonly string TempPath = Path.Combine(Path.GetTempPath(), "ExchangeSetupValidatorTemp");

		// Token: 0x04000039 RID: 57
		protected List<string> validatedFiles;

		// Token: 0x0400003A RID: 58
		private bool disposed;
	}
}
