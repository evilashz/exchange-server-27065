using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AAF RID: 2735
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValueNotPresentException : ADOperationException
	{
		// Token: 0x0600802E RID: 32814 RVA: 0x001A4EC3 File Offset: 0x001A30C3
		public ValueNotPresentException(string propertyName, string objectName) : base(DirectoryStrings.ExceptionValueNotPresent(propertyName, objectName))
		{
			this.propertyName = propertyName;
			this.objectName = objectName;
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x001A4EE0 File Offset: 0x001A30E0
		public ValueNotPresentException(string propertyName, string objectName, Exception innerException) : base(DirectoryStrings.ExceptionValueNotPresent(propertyName, objectName), innerException)
		{
			this.propertyName = propertyName;
			this.objectName = objectName;
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x001A4F00 File Offset: 0x001A3100
		protected ValueNotPresentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
			this.objectName = (string)info.GetValue("objectName", typeof(string));
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x001A4F55 File Offset: 0x001A3155
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
			info.AddValue("objectName", this.objectName);
		}

		// Token: 0x17002EC9 RID: 11977
		// (get) Token: 0x06008032 RID: 32818 RVA: 0x001A4F81 File Offset: 0x001A3181
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17002ECA RID: 11978
		// (get) Token: 0x06008033 RID: 32819 RVA: 0x001A4F89 File Offset: 0x001A3189
		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
		}

		// Token: 0x040055A3 RID: 21923
		private readonly string propertyName;

		// Token: 0x040055A4 RID: 21924
		private readonly string objectName;
	}
}
