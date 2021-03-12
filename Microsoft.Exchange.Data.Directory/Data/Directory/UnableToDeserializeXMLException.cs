using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0E RID: 2830
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToDeserializeXMLException : LocalizedException
	{
		// Token: 0x06008205 RID: 33285 RVA: 0x001A7B05 File Offset: 0x001A5D05
		public UnableToDeserializeXMLException(string errorStr) : base(DirectoryStrings.UnableToDeserializeXMLError(errorStr))
		{
			this.errorStr = errorStr;
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x001A7B1A File Offset: 0x001A5D1A
		public UnableToDeserializeXMLException(string errorStr, Exception innerException) : base(DirectoryStrings.UnableToDeserializeXMLError(errorStr), innerException)
		{
			this.errorStr = errorStr;
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x001A7B30 File Offset: 0x001A5D30
		protected UnableToDeserializeXMLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorStr = (string)info.GetValue("errorStr", typeof(string));
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x001A7B5A File Offset: 0x001A5D5A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorStr", this.errorStr);
		}

		// Token: 0x17002F24 RID: 12068
		// (get) Token: 0x06008209 RID: 33289 RVA: 0x001A7B75 File Offset: 0x001A5D75
		public string ErrorStr
		{
			get
			{
				return this.errorStr;
			}
		}

		// Token: 0x040055FE RID: 22014
		private readonly string errorStr;
	}
}
