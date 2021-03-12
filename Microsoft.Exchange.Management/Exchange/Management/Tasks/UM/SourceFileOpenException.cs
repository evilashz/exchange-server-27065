using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E1 RID: 4577
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceFileOpenException : LocalizedException
	{
		// Token: 0x0600B944 RID: 47428 RVA: 0x002A5B26 File Offset: 0x002A3D26
		public SourceFileOpenException(string fileName) : base(Strings.SourceFileOpenException(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x0600B945 RID: 47429 RVA: 0x002A5B3B File Offset: 0x002A3D3B
		public SourceFileOpenException(string fileName, Exception innerException) : base(Strings.SourceFileOpenException(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x0600B946 RID: 47430 RVA: 0x002A5B51 File Offset: 0x002A3D51
		protected SourceFileOpenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x0600B947 RID: 47431 RVA: 0x002A5B7B File Offset: 0x002A3D7B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x17003A3D RID: 14909
		// (get) Token: 0x0600B948 RID: 47432 RVA: 0x002A5B96 File Offset: 0x002A3D96
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x04006458 RID: 25688
		private readonly string fileName;
	}
}
