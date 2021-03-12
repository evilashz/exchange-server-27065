using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000910 RID: 2320
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		// Token: 0x06005F3B RID: 24379 RVA: 0x00147424 File Offset: 0x00145624
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06005F3C RID: 24380 RVA: 0x00147449 File Offset: 0x00145649
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x00147451 File Offset: 0x00145651
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06005F3E RID: 24382 RVA: 0x00147459 File Offset: 0x00145659
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06005F3F RID: 24383 RVA: 0x00147461 File Offset: 0x00145661
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002A6F RID: 10863
		internal int _major;

		// Token: 0x04002A70 RID: 10864
		internal int _minor;

		// Token: 0x04002A71 RID: 10865
		internal int _build;

		// Token: 0x04002A72 RID: 10866
		internal int _revision;
	}
}
