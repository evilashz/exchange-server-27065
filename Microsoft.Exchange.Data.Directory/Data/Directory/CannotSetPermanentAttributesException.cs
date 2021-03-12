using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B12 RID: 2834
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetPermanentAttributesException : DataSourceOperationException
	{
		// Token: 0x06008219 RID: 33305 RVA: 0x001A7CE9 File Offset: 0x001A5EE9
		public CannotSetPermanentAttributesException(string permanentAttributeNames) : base(DirectoryStrings.ErrorCannotSetPermanentAttributes(permanentAttributeNames))
		{
			this.permanentAttributeNames = permanentAttributeNames;
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x001A7CFE File Offset: 0x001A5EFE
		public CannotSetPermanentAttributesException(string permanentAttributeNames, Exception innerException) : base(DirectoryStrings.ErrorCannotSetPermanentAttributes(permanentAttributeNames), innerException)
		{
			this.permanentAttributeNames = permanentAttributeNames;
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x001A7D14 File Offset: 0x001A5F14
		protected CannotSetPermanentAttributesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.permanentAttributeNames = (string)info.GetValue("permanentAttributeNames", typeof(string));
		}

		// Token: 0x0600821C RID: 33308 RVA: 0x001A7D3E File Offset: 0x001A5F3E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("permanentAttributeNames", this.permanentAttributeNames);
		}

		// Token: 0x17002F28 RID: 12072
		// (get) Token: 0x0600821D RID: 33309 RVA: 0x001A7D59 File Offset: 0x001A5F59
		public string PermanentAttributeNames
		{
			get
			{
				return this.permanentAttributeNames;
			}
		}

		// Token: 0x04005602 RID: 22018
		private readonly string permanentAttributeNames;
	}
}
