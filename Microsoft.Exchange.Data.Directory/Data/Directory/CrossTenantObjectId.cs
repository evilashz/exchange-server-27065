using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public sealed class CrossTenantObjectId : ObjectId, ITraceable, IEquatable<CrossTenantObjectId>
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00034F8C File Offset: 0x0003318C
		internal CrossTenantObjectId(byte format, Guid externalDirectoryOrganizationId, Guid externalDirectoryObjectId)
		{
			if (externalDirectoryOrganizationId == Guid.Empty)
			{
				throw new ArgumentException("A valid ExternalDirectoryOrganizationId is required to create a link to an object using external directory information.", "externalDirectoryOrganizationId");
			}
			if (externalDirectoryObjectId == Guid.Empty)
			{
				throw new ArgumentException("A valid ExternalDirectoryObjectId is required to create a link to an object using external directory information.", "externalDirectoryObjectId");
			}
			this.format = format;
			this.externalDirectoryOrganizationId = externalDirectoryOrganizationId;
			this.externalDirectoryObjectId = externalDirectoryObjectId;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00034FEE File Offset: 0x000331EE
		public byte Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00034FF6 File Offset: 0x000331F6
		public Guid ExternalDirectoryObjectId
		{
			get
			{
				return this.externalDirectoryObjectId;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00034FFE File Offset: 0x000331FE
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return this.externalDirectoryOrganizationId;
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00035006 File Offset: 0x00033206
		public static CrossTenantObjectId FromExternalDirectoryIds(Guid externalDirectoryOrganizationId, Guid externalDirectoryObjectId)
		{
			return new CrossTenantObjectId(1, externalDirectoryOrganizationId, externalDirectoryObjectId);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00035010 File Offset: 0x00033210
		public static CrossTenantObjectId Parse(byte[] input)
		{
			return CrossTenantObjectId.Parse(input, false);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003501C File Offset: 0x0003321C
		public static CrossTenantObjectId Parse(byte[] input, bool localizedException)
		{
			int num = 0;
			byte b = input[num++];
			byte b2 = b;
			if (b2 == 1)
			{
				if (input.Length != 33)
				{
					throw new FormatException(DirectoryStrings.InvalidCrossTenantIdFormat(BitConverter.ToString(input)));
				}
				byte[] array = new byte[16];
				Array.Copy(input, num, array, 0, 16);
				num += 16;
				Guid guid = new Guid(array);
				Array.Copy(input, num, array, 0, 16);
				Guid guid2 = new Guid(array);
				return new CrossTenantObjectId(b, guid, guid2);
			}
			else
			{
				string str = BitConverter.ToString(input);
				if (localizedException)
				{
					throw new InvalidCrossTenantIdFormatException(str);
				}
				throw new FormatException(DirectoryStrings.InvalidCrossTenantIdFormat(str));
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000350B9 File Offset: 0x000332B9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CrossTenantObjectId);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000350C7 File Offset: 0x000332C7
		public bool Equals(CrossTenantObjectId other)
		{
			return other != null && this.ExternalDirectoryOrganizationId == other.ExternalDirectoryOrganizationId && this.ExternalDirectoryObjectId == other.ExternalDirectoryObjectId;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000350F4 File Offset: 0x000332F4
		public override byte[] GetBytes()
		{
			byte[] array = new byte[33];
			int num = 0;
			array[num++] = this.format;
			byte[] sourceArray = this.ExternalDirectoryOrganizationId.ToByteArray();
			Array.Copy(sourceArray, 0, array, num, 16);
			num += 16;
			sourceArray = this.ExternalDirectoryObjectId.ToByteArray();
			Array.Copy(sourceArray, 0, array, num, 16);
			return array;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00035154 File Offset: 0x00033354
		public override int GetHashCode()
		{
			return (int)this.format ^ this.ExternalDirectoryOrganizationId.GetHashCode() ^ this.ExternalDirectoryObjectId.GetHashCode();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00035194 File Offset: 0x00033394
		public override string ToString()
		{
			return string.Format("{0}{{{1}}}({2},{3})", new object[]
			{
				CrossTenantObjectId.typeName,
				this.format,
				this.ExternalDirectoryOrganizationId,
				this.ExternalDirectoryObjectId
			});
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000351E5 File Offset: 0x000333E5
		public void TraceTo(ITraceBuilder traceBuilder)
		{
			traceBuilder.AddArgument(this.ToString());
		}

		// Token: 0x040005C2 RID: 1474
		private const int BytesForGuid = 16;

		// Token: 0x040005C3 RID: 1475
		private const int ExternalDirectoryFormatLength = 33;

		// Token: 0x040005C4 RID: 1476
		private static readonly string typeName = typeof(CrossTenantObjectId).Name;

		// Token: 0x040005C5 RID: 1477
		private readonly Guid externalDirectoryObjectId;

		// Token: 0x040005C6 RID: 1478
		private readonly Guid externalDirectoryOrganizationId;

		// Token: 0x040005C7 RID: 1479
		private readonly byte format;

		// Token: 0x02000110 RID: 272
		private enum CrossTenantObjectIdFormat : byte
		{
			// Token: 0x040005C9 RID: 1481
			ExternalDirectory = 1
		}
	}
}
