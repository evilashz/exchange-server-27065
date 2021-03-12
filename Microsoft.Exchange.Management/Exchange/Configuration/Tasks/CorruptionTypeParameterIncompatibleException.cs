using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001148 RID: 4424
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptionTypeParameterIncompatibleException : LocalizedException
	{
		// Token: 0x0600B54D RID: 46413 RVA: 0x0029DFD5 File Offset: 0x0029C1D5
		public CorruptionTypeParameterIncompatibleException(string paramName) : base(Strings.CorruptionTypeParameterIncompatible(paramName))
		{
			this.paramName = paramName;
		}

		// Token: 0x0600B54E RID: 46414 RVA: 0x0029DFEA File Offset: 0x0029C1EA
		public CorruptionTypeParameterIncompatibleException(string paramName, Exception innerException) : base(Strings.CorruptionTypeParameterIncompatible(paramName), innerException)
		{
			this.paramName = paramName;
		}

		// Token: 0x0600B54F RID: 46415 RVA: 0x0029E000 File Offset: 0x0029C200
		protected CorruptionTypeParameterIncompatibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.paramName = (string)info.GetValue("paramName", typeof(string));
		}

		// Token: 0x0600B550 RID: 46416 RVA: 0x0029E02A File Offset: 0x0029C22A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("paramName", this.paramName);
		}

		// Token: 0x1700394A RID: 14666
		// (get) Token: 0x0600B551 RID: 46417 RVA: 0x0029E045 File Offset: 0x0029C245
		public string ParamName
		{
			get
			{
				return this.paramName;
			}
		}

		// Token: 0x040062B0 RID: 25264
		private readonly string paramName;
	}
}
