using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007F RID: 127
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SetupNotFoundInSourceDirException : LocalizedException
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00016756 File Offset: 0x00014956
		public SetupNotFoundInSourceDirException(string fileName) : base(Strings.SetupNotFoundInSourceDirError(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001676B File Offset: 0x0001496B
		public SetupNotFoundInSourceDirException(string fileName, Exception innerException) : base(Strings.SetupNotFoundInSourceDirError(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00016781 File Offset: 0x00014981
		protected SetupNotFoundInSourceDirException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000167AB File Offset: 0x000149AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000167C6 File Offset: 0x000149C6
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x040002FD RID: 765
		private readonly string fileName;
	}
}
