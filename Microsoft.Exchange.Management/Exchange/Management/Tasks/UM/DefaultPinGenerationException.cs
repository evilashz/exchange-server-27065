using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B4 RID: 4532
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultPinGenerationException : LocalizedException
	{
		// Token: 0x0600B86E RID: 47214 RVA: 0x002A491C File Offset: 0x002A2B1C
		public DefaultPinGenerationException() : base(Strings.ErrorGeneratingDefaultPassword)
		{
		}

		// Token: 0x0600B86F RID: 47215 RVA: 0x002A4929 File Offset: 0x002A2B29
		public DefaultPinGenerationException(Exception innerException) : base(Strings.ErrorGeneratingDefaultPassword, innerException)
		{
		}

		// Token: 0x0600B870 RID: 47216 RVA: 0x002A4937 File Offset: 0x002A2B37
		protected DefaultPinGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B871 RID: 47217 RVA: 0x002A4941 File Offset: 0x002A2B41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
