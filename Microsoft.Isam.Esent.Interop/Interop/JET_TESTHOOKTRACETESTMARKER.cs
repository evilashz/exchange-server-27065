using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class JET_TESTHOOKTRACETESTMARKER : IContentEquatable<JET_TESTHOOKTRACETESTMARKER>, IDeepCloneable<JET_TESTHOOKTRACETESTMARKER>
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000BE26 File Offset: 0x0000A026
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000BE2E File Offset: 0x0000A02E
		public ulong qwMarkerID
		{
			[DebuggerStepThrough]
			get
			{
				return this.markerId;
			}
			set
			{
				this.markerId = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000BE37 File Offset: 0x0000A037
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000BE3F File Offset: 0x0000A03F
		public string szAnnotation
		{
			[DebuggerStepThrough]
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000BE48 File Offset: 0x0000A048
		public bool ContentEquals(JET_TESTHOOKTRACETESTMARKER other)
		{
			return other != null && this.qwMarkerID == other.qwMarkerID && string.Equals(this.szAnnotation, other.szAnnotation);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000BE80 File Offset: 0x0000A080
		public JET_TESTHOOKTRACETESTMARKER DeepClone()
		{
			return (JET_TESTHOOKTRACETESTMARKER)base.MemberwiseClone();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TESTHOOKTRACETESTMARKER({0}:{1})", new object[]
			{
				this.qwMarkerID,
				this.szAnnotation
			});
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		internal NATIVE_TESTHOOKTRACETESTMARKER GetNativeTraceTestMarker(ref GCHandleCollection handles)
		{
			return new NATIVE_TESTHOOKTRACETESTMARKER
			{
				cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_TESTHOOKTRACETESTMARKER))),
				qwMarkerID = this.qwMarkerID,
				szAnnotation = handles.Add(Util.ConvertToNullTerminatedAsciiByteArray(this.szAnnotation))
			};
		}

		// Token: 0x040001F9 RID: 505
		private ulong markerId;

		// Token: 0x040001FA RID: 506
		private string annotation;
	}
}
