using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200020C RID: 524
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PropTagHelper
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x0002B930 File Offset: 0x00029B30
		public static PropTag PropTagFromIdAndType(int propId, PropType propType)
		{
			return (PropTag)(propId << 16 | (int)propType);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002B938 File Offset: 0x00029B38
		[Obsolete]
		public static int PropTagToId(PropTag propTag)
		{
			return propTag.Id();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002B940 File Offset: 0x00029B40
		[Obsolete]
		public static PropType PropTagToType(PropTag propTag)
		{
			return propTag.ValueType();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002B948 File Offset: 0x00029B48
		public static int Id(this PropTag propTag)
		{
			return (int)((propTag & (PropTag)4294901760U) >> 16);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002B954 File Offset: 0x00029B54
		public static PropType ValueType(this PropTag propTag)
		{
			return (PropType)(propTag & (PropTag)65535U);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002B95D File Offset: 0x00029B5D
		public static PropTag ChangePropType(this PropTag propTag, PropType propType)
		{
			return PropTagHelper.PropTagFromIdAndType(propTag.Id(), propType);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002B96B File Offset: 0x00029B6B
		public static bool IsMultiValued(this PropTag propTag)
		{
			return (propTag.ValueType() & PropType.MultiValueFlag) != PropType.Unspecified;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002B97F File Offset: 0x00029B7F
		public static bool IsMultiInstance(this PropTag propTag)
		{
			return (propTag.ValueType() & PropType.MultiInstanceFlag) != PropType.Unspecified;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002B993 File Offset: 0x00029B93
		public static bool IsNamedProperty(this PropTag propTag)
		{
			return propTag.Id() >= 32768 && propTag.Id() <= 65534;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		public static bool IsApplicationSpecific(this PropTag propTag)
		{
			return propTag.Id() >= 24576 && propTag.Id() <= 32767;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002B9D5 File Offset: 0x00029BD5
		public static bool IsValid(this PropTag propTag)
		{
			return propTag.Id() <= 32767;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		private static bool IsNotTransmittable(this PropTag propTag)
		{
			int num = propTag.Id();
			return (num >= 3584 && num <= 4095) || (num >= 24576 && num <= 26111) || (num >= 26112 && num <= 26623) || (num >= 31744 && num <= 32761) || num == PropTag.NativeBodyInfo.Id();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002BA4E File Offset: 0x00029C4E
		public static bool IsTransmittable(this PropTag propTag)
		{
			return !propTag.IsNotTransmittable();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002BA5C File Offset: 0x00029C5C
		internal static PropTag[] SPropTagArray(ICollection<PropTag> tags)
		{
			if (tags == null)
			{
				return null;
			}
			PropTag[] array = new PropTag[tags.Count + 1];
			tags.CopyTo(array, 1);
			array[0] = (PropTag)tags.Count;
			return array;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002BA90 File Offset: 0x00029C90
		public static PropTag[] PropTagArray(byte[] blob)
		{
			if (blob == null)
			{
				return null;
			}
			if (blob.Length % 4 != 0)
			{
				throw new ArgumentException("blob", "Invalid blob size.");
			}
			int num = blob.Length / 4;
			PropTag[] array = new PropTag[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (PropTag)BitConverter.ToUInt32(blob, i * 4);
			}
			return array;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		public static PropTag[] PropTagArray(IntPtr blob)
		{
			if (blob == IntPtr.Zero)
			{
				return null;
			}
			int num = 0;
			int num2 = Marshal.ReadInt32(blob, num);
			PropTag[] array = new PropTag[num2];
			for (int i = 0; i < num2; i++)
			{
				num += 4;
				array[i] = (PropTag)Marshal.ReadInt32(blob, num);
			}
			return array;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002BB28 File Offset: 0x00029D28
		public static PropTag ConvertToError(PropTag prop)
		{
			return PropTagHelper.PropTagFromIdAndType(prop.Id(), PropType.Error);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002BB37 File Offset: 0x00029D37
		public static int GetBytesToMarshal(ICollection<PropTag> props)
		{
			return (props.Count + 1) * 4;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002BB44 File Offset: 0x00029D44
		public static void MarshalToNative(ICollection<PropTag> props, IntPtr blob)
		{
			int num = 0;
			Marshal.WriteInt32(blob, num, props.Count);
			foreach (PropTag val in props)
			{
				num += 4;
				Marshal.WriteInt32(blob, num, (int)val);
			}
		}

		// Token: 0x04000F35 RID: 3893
		public const int MultiValuedFlag = 4096;

		// Token: 0x04000F36 RID: 3894
		public const int MultiValuedInstanceFlag = 12288;

		// Token: 0x04000F37 RID: 3895
		private const int MinUserDefinedNamed = 32768;

		// Token: 0x04000F38 RID: 3896
		private const int MaxUserDefinedNamed = 65534;

		// Token: 0x04000F39 RID: 3897
		private const int MinApplicationSpecificPropertyTag = 26624;

		// Token: 0x04000F3A RID: 3898
		private const int MaxApplicationSpecificPropertyTag = 32767;

		// Token: 0x04000F3B RID: 3899
		private const int MinMapiNonTransmittable = 3584;

		// Token: 0x04000F3C RID: 3900
		private const int MaxMapiNonTransmittable = 4095;

		// Token: 0x04000F3D RID: 3901
		private const int MinUserDefinedNonTransmittable = 24576;

		// Token: 0x04000F3E RID: 3902
		private const int MaxUserDefinedNonTransmittable = 26111;

		// Token: 0x04000F3F RID: 3903
		private const int MinProviderDefinedNonTransmittable = 26112;

		// Token: 0x04000F40 RID: 3904
		private const int MaxProviderDefinedNonTransmittable = 26623;

		// Token: 0x04000F41 RID: 3905
		private const int MinMessageClassDefinedNonTransmittable = 31744;

		// Token: 0x04000F42 RID: 3906
		private const int MaxMessageClassDefinedNonTransmittable = 32761;
	}
}
