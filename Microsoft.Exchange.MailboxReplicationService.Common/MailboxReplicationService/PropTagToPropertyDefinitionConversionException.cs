using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000366 RID: 870
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropTagToPropertyDefinitionConversionException : MailboxReplicationPermanentException
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x00053952 File Offset: 0x00051B52
		public PropTagToPropertyDefinitionConversionException(int propTag) : base(MrsStrings.PropTagToPropertyDefinitionConversion(propTag))
		{
			this.propTag = propTag;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00053967 File Offset: 0x00051B67
		public PropTagToPropertyDefinitionConversionException(int propTag, Exception innerException) : base(MrsStrings.PropTagToPropertyDefinitionConversion(propTag), innerException)
		{
			this.propTag = propTag;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0005397D File Offset: 0x00051B7D
		protected PropTagToPropertyDefinitionConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propTag = (int)info.GetValue("propTag", typeof(int));
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000539A7 File Offset: 0x00051BA7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propTag", this.propTag);
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000539C2 File Offset: 0x00051BC2
		public int PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x0400106C RID: 4204
		private readonly int propTag;
	}
}
