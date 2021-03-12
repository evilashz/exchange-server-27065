using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001BB RID: 443
	internal sealed class TypeLocation : NotificationLocation
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003CD04 File Offset: 0x0003AF04
		public TypeLocation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003CD27 File Offset: 0x0003AF27
		public override KeyValuePair<string, object> GetEventData()
		{
			return new KeyValuePair<string, object>("Type", this.type.Name);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003CD3E File Offset: 0x0003AF3E
		public override int GetHashCode()
		{
			return TypeLocation.TypeHashCode ^ this.type.GetHashCode();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003CD54 File Offset: 0x0003AF54
		public override bool Equals(object obj)
		{
			TypeLocation typeLocation = obj as TypeLocation;
			return typeLocation != null && this.type.Equals(typeLocation.type);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003CD7E File Offset: 0x0003AF7E
		public override string ToString()
		{
			return this.type.Name;
		}

		// Token: 0x0400096B RID: 2411
		private const string EventKey = "Type";

		// Token: 0x0400096C RID: 2412
		private static readonly int TypeHashCode = typeof(TypeLocation).GetHashCode();

		// Token: 0x0400096D RID: 2413
		private readonly Type type;
	}
}
