using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001125 RID: 4389
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CredentialNotSupportedForEndpointTypeException : LocalizedException
	{
		// Token: 0x0600B4A8 RID: 46248 RVA: 0x0029D229 File Offset: 0x0029B429
		public CredentialNotSupportedForEndpointTypeException(string endpointType) : base(Strings.ErrorCredentialNotSupportedForEndpointType(endpointType))
		{
			this.endpointType = endpointType;
		}

		// Token: 0x0600B4A9 RID: 46249 RVA: 0x0029D23E File Offset: 0x0029B43E
		public CredentialNotSupportedForEndpointTypeException(string endpointType, Exception innerException) : base(Strings.ErrorCredentialNotSupportedForEndpointType(endpointType), innerException)
		{
			this.endpointType = endpointType;
		}

		// Token: 0x0600B4AA RID: 46250 RVA: 0x0029D254 File Offset: 0x0029B454
		protected CredentialNotSupportedForEndpointTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointType = (string)info.GetValue("endpointType", typeof(string));
		}

		// Token: 0x0600B4AB RID: 46251 RVA: 0x0029D27E File Offset: 0x0029B47E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointType", this.endpointType);
		}

		// Token: 0x17003931 RID: 14641
		// (get) Token: 0x0600B4AC RID: 46252 RVA: 0x0029D299 File Offset: 0x0029B499
		public string EndpointType
		{
			get
			{
				return this.endpointType;
			}
		}

		// Token: 0x04006297 RID: 25239
		private readonly string endpointType;
	}
}
