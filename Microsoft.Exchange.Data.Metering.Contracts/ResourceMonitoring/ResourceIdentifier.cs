using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000013 RID: 19
	internal sealed class ResourceIdentifier : IEquatable<ResourceIdentifier>
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002240 File Offset: 0x00000440
		internal ResourceIdentifier(string name, string instanceName = "")
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			ArgumentValidator.ThrowIfNull("instanceName", instanceName);
			if (!ResourceIdentifier.NamePattern.IsMatch(name))
			{
				throw new ArgumentException("The resource name should contain only letters and digits", name);
			}
			this.Name = name;
			this.InstanceName = instanceName;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002298 File Offset: 0x00000498
		internal string Name { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000022A1 File Offset: 0x000004A1
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000022A9 File Offset: 0x000004A9
		internal string InstanceName { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x000022B4 File Offset: 0x000004B4
		public static bool TryParse(string resourceString, out ResourceIdentifier resourceIdentifier)
		{
			resourceIdentifier = null;
			if (string.IsNullOrEmpty(resourceString))
			{
				return false;
			}
			Match match = ResourceIdentifier.ParsePattern.Match(resourceString);
			if (match.Success)
			{
				if (match.Groups["name"].Success && match.Groups["instance"].Success)
				{
					resourceIdentifier = new ResourceIdentifier(match.Groups["name"].Value, match.Groups["instance"].Value);
				}
				else
				{
					resourceIdentifier = new ResourceIdentifier(match.Groups["name"].Value, "");
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000236A File Offset: 0x0000056A
		public static bool operator ==(ResourceIdentifier obj1, ResourceIdentifier obj2)
		{
			return object.ReferenceEquals(obj1, obj2) || (!object.ReferenceEquals(obj1, null) && !object.ReferenceEquals(obj2, null) && obj1.Equals(obj2));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002392 File Offset: 0x00000592
		public static bool operator !=(ResourceIdentifier obj1, ResourceIdentifier obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000023A0 File Offset: 0x000005A0
		public bool Equals(ResourceIdentifier other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(this, other) || (!(base.GetType() != other.GetType()) && string.Equals(this.InstanceName, other.InstanceName, StringComparison.OrdinalIgnoreCase) && this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002400 File Offset: 0x00000600
		public override bool Equals(object other)
		{
			return this.Equals(other as ResourceIdentifier);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002410 File Offset: 0x00000610
		public override int GetHashCode()
		{
			int num = 17 + 31 * this.Name.ToLower().GetHashCode();
			if (!string.IsNullOrEmpty(this.InstanceName))
			{
				num += 31 * this.InstanceName.ToLower().GetHashCode();
			}
			return num;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002458 File Offset: 0x00000658
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.InstanceName))
			{
				return this.Name;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[]
			{
				this.Name,
				this.InstanceName
			});
		}

		// Token: 0x04000014 RID: 20
		private static readonly Regex NamePattern = new Regex("^\\w+$");

		// Token: 0x04000015 RID: 21
		private static readonly Regex ParsePattern = new Regex("(?<name>^\\w+)(\\[(?<instance>.+)\\])?$");
	}
}
