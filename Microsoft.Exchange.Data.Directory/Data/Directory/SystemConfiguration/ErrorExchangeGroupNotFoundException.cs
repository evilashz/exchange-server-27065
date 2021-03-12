using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000ACA RID: 2762
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorExchangeGroupNotFoundException : LocalizedException
	{
		// Token: 0x060080B4 RID: 32948 RVA: 0x001A5B80 File Offset: 0x001A3D80
		public ErrorExchangeGroupNotFoundException(Guid idStringValue) : base(DirectoryStrings.ErrorExchangeGroupNotFound(idStringValue))
		{
			this.idStringValue = idStringValue;
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x001A5B95 File Offset: 0x001A3D95
		public ErrorExchangeGroupNotFoundException(Guid idStringValue, Exception innerException) : base(DirectoryStrings.ErrorExchangeGroupNotFound(idStringValue), innerException)
		{
			this.idStringValue = idStringValue;
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x001A5BAB File Offset: 0x001A3DAB
		protected ErrorExchangeGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.idStringValue = (Guid)info.GetValue("idStringValue", typeof(Guid));
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x001A5BD5 File Offset: 0x001A3DD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("idStringValue", this.idStringValue);
		}

		// Token: 0x17002EE3 RID: 12003
		// (get) Token: 0x060080B8 RID: 32952 RVA: 0x001A5BF5 File Offset: 0x001A3DF5
		public Guid IdStringValue
		{
			get
			{
				return this.idStringValue;
			}
		}

		// Token: 0x040055BD RID: 21949
		private readonly Guid idStringValue;
	}
}
