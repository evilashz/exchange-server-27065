using System;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000017 RID: 23
	internal sealed class DiskSpaceValidator : ValidatorBase
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000349D File Offset: 0x0000169D
		public DiskSpaceValidator(long requiredSpace, string path) : this(requiredSpace, path, null)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000034A8 File Offset: 0x000016A8
		public DiskSpaceValidator(long requiredSpace, string path, Action<object> callback) : this(requiredSpace, path, true, callback)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034B4 File Offset: 0x000016B4
		public DiskSpaceValidator(long requiredSpace, string path, bool checkUserTemp, Action<object> callback)
		{
			this.requiredSpace = requiredSpace;
			this.path = path;
			this.checkUserTemp = checkUserTemp;
			base.Callback = callback;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000034DC File Offset: 0x000016DC
		public override bool Validate()
		{
			base.InvokeCallback(Strings.CheckForAvailableSpace);
			ValidationHelper.ThrowIfNullOrEmpty(this.path, "this.path");
			if (!Directory.Exists(this.path))
			{
				base.InvokeCallback(Strings.NotExist(this.path));
				return false;
			}
			DriveInfo driveInfo = new DriveInfo(this.path);
			if (driveInfo.AvailableFreeSpace < this.requiredSpace)
			{
				return false;
			}
			if (this.checkUserTemp)
			{
				DriveInfo driveInfo2 = new DriveInfo(Path.GetTempPath());
				if (driveInfo2.AvailableFreeSpace < this.requiredSpace)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400003C RID: 60
		private readonly long requiredSpace;

		// Token: 0x0400003D RID: 61
		private readonly string path;

		// Token: 0x0400003E RID: 62
		private readonly bool checkUserTemp;
	}
}
