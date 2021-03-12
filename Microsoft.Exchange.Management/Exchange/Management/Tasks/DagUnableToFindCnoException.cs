using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001075 RID: 4213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagUnableToFindCnoException : LocalizedException
	{
		// Token: 0x0600B12C RID: 45356 RVA: 0x00297945 File Offset: 0x00295B45
		public DagUnableToFindCnoException(string cnoName) : base(Strings.DagUnableToFindCnoError(cnoName))
		{
			this.cnoName = cnoName;
		}

		// Token: 0x0600B12D RID: 45357 RVA: 0x0029795A File Offset: 0x00295B5A
		public DagUnableToFindCnoException(string cnoName, Exception innerException) : base(Strings.DagUnableToFindCnoError(cnoName), innerException)
		{
			this.cnoName = cnoName;
		}

		// Token: 0x0600B12E RID: 45358 RVA: 0x00297970 File Offset: 0x00295B70
		protected DagUnableToFindCnoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cnoName = (string)info.GetValue("cnoName", typeof(string));
		}

		// Token: 0x0600B12F RID: 45359 RVA: 0x0029799A File Offset: 0x00295B9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cnoName", this.cnoName);
		}

		// Token: 0x17003875 RID: 14453
		// (get) Token: 0x0600B130 RID: 45360 RVA: 0x002979B5 File Offset: 0x00295BB5
		public string CnoName
		{
			get
			{
				return this.cnoName;
			}
		}

		// Token: 0x040061DB RID: 25051
		private readonly string cnoName;
	}
}
