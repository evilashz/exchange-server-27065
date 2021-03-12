using System;
using System.Globalization;
using System.IO;
using System.Security;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000076 RID: 118
	internal class ConfigFileFingerPrint : IEquatable<ConfigFileFingerPrint>
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00008A10 File Offset: 0x00006C10
		internal ConfigFileFingerPrint(string filePath)
		{
			Exception ex = null;
			bool flag = false;
			try
			{
				this.filePath = filePath;
				if (File.Exists(filePath))
				{
					FileInfo fileInfo = new FileInfo(filePath);
					this.CreatedTimeUtc = fileInfo.CreationTimeUtc;
					this.ModifiedTimeUtc = fileInfo.LastWriteTimeUtc;
					this.Size = fileInfo.Length;
					this.fileExists = true;
				}
				return;
			}
			catch (NotSupportedException ex2)
			{
				flag = true;
				ex = ex2;
			}
			catch (PathTooLongException ex3)
			{
				flag = true;
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				flag = true;
				ex = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				flag = true;
				ex = ex5;
			}
			catch (SecurityException ex6)
			{
				flag = true;
				ex = ex6;
			}
			catch (DirectoryNotFoundException ex7)
			{
				ex = ex7;
			}
			catch (FileNotFoundException ex8)
			{
				ex = ex8;
			}
			catch (IOException ex9)
			{
				flag = true;
				ex = ex9;
			}
			if (ex != null)
			{
				this.fileExists = false;
				if (flag)
				{
					InternalBypassTrace.FaultInjectionConfigurationTracer.TraceError(0, 0L, "Unexpected exception reading file {0}, Exception={1}", new object[]
					{
						this.filePath,
						ex
					});
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008B48 File Offset: 0x00006D48
		private ConfigFileFingerPrint()
		{
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00008B50 File Offset: 0x00006D50
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00008B58 File Offset: 0x00006D58
		internal long Size { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008B61 File Offset: 0x00006D61
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00008B69 File Offset: 0x00006D69
		internal DateTime CreatedTimeUtc { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008B72 File Offset: 0x00006D72
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00008B7A File Offset: 0x00006D7A
		internal DateTime ModifiedTimeUtc { get; private set; }

		// Token: 0x0600028B RID: 651 RVA: 0x00008B84 File Offset: 0x00006D84
		public bool Equals(ConfigFileFingerPrint other)
		{
			return other != null && (this.fileExists == other.fileExists && StringComparer.OrdinalIgnoreCase.Equals(this.filePath, other.filePath) && this.Size == other.Size && this.ModifiedTimeUtc == other.ModifiedTimeUtc) && this.CreatedTimeUtc == other.CreatedTimeUtc;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public override bool Equals(object obj)
		{
			ConfigFileFingerPrint other = obj as ConfigFileFingerPrint;
			return this.Equals(other);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008C0B File Offset: 0x00006E0B
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00008C18 File Offset: 0x00006E18
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.traceValue))
			{
				if (this.fileExists)
				{
					this.traceValue = string.Format(CultureInfo.InvariantCulture, "File={0}, Created={1}, Modified={2}, Size={3}", new object[]
					{
						this.filePath,
						this.CreatedTimeUtc,
						this.ModifiedTimeUtc,
						this.Size
					});
				}
				else
				{
					this.traceValue = string.Format(CultureInfo.InvariantCulture, "File={0}, Does not exist", new object[]
					{
						this.filePath
					});
				}
			}
			return this.traceValue;
		}

		// Token: 0x04000254 RID: 596
		private string traceValue;

		// Token: 0x04000255 RID: 597
		private string filePath;

		// Token: 0x04000256 RID: 598
		private bool fileExists;
	}
}
