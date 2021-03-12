using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200007F RID: 127
	internal class Signature : IEquatable<Signature>
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this.properties = properties.ToArray<DynamicProperty>();
			this.hashCode = 0;
			foreach (DynamicProperty dynamicProperty in properties)
			{
				this.hashCode ^= (dynamicProperty.Name.GetHashCode() ^ dynamicProperty.Type.GetHashCode());
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public override bool Equals(object obj)
		{
			return obj is Signature && this.Equals((Signature)obj);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public bool Equals(Signature other)
		{
			if (this.properties.Length != other.properties.Length)
			{
				return false;
			}
			for (int i = 0; i < this.properties.Length; i++)
			{
				if (this.properties[i].Name != other.properties[i].Name || this.properties[i].Type != other.properties[i].Type)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400011B RID: 283
		public DynamicProperty[] properties;

		// Token: 0x0400011C RID: 284
		public int hashCode;
	}
}
