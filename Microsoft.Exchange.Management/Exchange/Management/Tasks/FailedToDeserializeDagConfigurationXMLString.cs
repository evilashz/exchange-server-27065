using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001042 RID: 4162
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDeserializeDagConfigurationXMLString : LocalizedException
	{
		// Token: 0x0600B005 RID: 45061 RVA: 0x00295469 File Offset: 0x00293669
		public FailedToDeserializeDagConfigurationXMLString(string stringToDeserialize, string typeName) : base(Strings.FailedToDeserializeDagConfigurationXMLString(stringToDeserialize, typeName))
		{
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
		}

		// Token: 0x0600B006 RID: 45062 RVA: 0x00295486 File Offset: 0x00293686
		public FailedToDeserializeDagConfigurationXMLString(string stringToDeserialize, string typeName, Exception innerException) : base(Strings.FailedToDeserializeDagConfigurationXMLString(stringToDeserialize, typeName), innerException)
		{
			this.stringToDeserialize = stringToDeserialize;
			this.typeName = typeName;
		}

		// Token: 0x0600B007 RID: 45063 RVA: 0x002954A4 File Offset: 0x002936A4
		protected FailedToDeserializeDagConfigurationXMLString(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.stringToDeserialize = (string)info.GetValue("stringToDeserialize", typeof(string));
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x0600B008 RID: 45064 RVA: 0x002954F9 File Offset: 0x002936F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("stringToDeserialize", this.stringToDeserialize);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x1700381A RID: 14362
		// (get) Token: 0x0600B009 RID: 45065 RVA: 0x00295525 File Offset: 0x00293725
		public string StringToDeserialize
		{
			get
			{
				return this.stringToDeserialize;
			}
		}

		// Token: 0x1700381B RID: 14363
		// (get) Token: 0x0600B00A RID: 45066 RVA: 0x0029552D File Offset: 0x0029372D
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x04006180 RID: 24960
		private readonly string stringToDeserialize;

		// Token: 0x04006181 RID: 24961
		private readonly string typeName;
	}
}
