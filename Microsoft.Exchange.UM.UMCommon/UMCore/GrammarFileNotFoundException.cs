using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021E RID: 542
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class GrammarFileNotFoundException : LocalizedException
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x00039E6C File Offset: 0x0003806C
		public GrammarFileNotFoundException(string grammarFile) : base(Strings.GrammarFileNotFoundException(grammarFile))
		{
			this.grammarFile = grammarFile;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00039E81 File Offset: 0x00038081
		public GrammarFileNotFoundException(string grammarFile, Exception innerException) : base(Strings.GrammarFileNotFoundException(grammarFile), innerException)
		{
			this.grammarFile = grammarFile;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00039E97 File Offset: 0x00038097
		protected GrammarFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.grammarFile = (string)info.GetValue("grammarFile", typeof(string));
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00039EC1 File Offset: 0x000380C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("grammarFile", this.grammarFile);
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00039EDC File Offset: 0x000380DC
		public string GrammarFile
		{
			get
			{
				return this.grammarFile;
			}
		}

		// Token: 0x04000893 RID: 2195
		private readonly string grammarFile;
	}
}
