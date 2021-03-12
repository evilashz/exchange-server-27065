using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000203 RID: 515
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToCreateCallerPropertiesException : LocalizedException
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x000393F0 File Offset: 0x000375F0
		public UnableToCreateCallerPropertiesException(string typeA) : base(Strings.UnableToCreateCallerPropertiesException(typeA))
		{
			this.typeA = typeA;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00039405 File Offset: 0x00037605
		public UnableToCreateCallerPropertiesException(string typeA, Exception innerException) : base(Strings.UnableToCreateCallerPropertiesException(typeA), innerException)
		{
			this.typeA = typeA;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003941B File Offset: 0x0003761B
		protected UnableToCreateCallerPropertiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.typeA = (string)info.GetValue("typeA", typeof(string));
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00039445 File Offset: 0x00037645
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typeA", this.typeA);
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00039460 File Offset: 0x00037660
		public string TypeA
		{
			get
			{
				return this.typeA;
			}
		}

		// Token: 0x04000880 RID: 2176
		private readonly string typeA;
	}
}
