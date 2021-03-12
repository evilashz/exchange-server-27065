using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FB3 RID: 4019
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisTasksValidationInvalidVirtualDirectoryCharException : LocalizedException
	{
		// Token: 0x0600AD55 RID: 44373 RVA: 0x00291856 File Offset: 0x0028FA56
		public IisTasksValidationInvalidVirtualDirectoryCharException(string virtualDirectory, char badChar, int charIndex, char[] invalidChars) : base(Strings.IisTasksValidationInvalidVirtualDirectoryCharException(virtualDirectory, badChar, charIndex, invalidChars))
		{
			this.virtualDirectory = virtualDirectory;
			this.badChar = badChar;
			this.charIndex = charIndex;
			this.invalidChars = invalidChars;
		}

		// Token: 0x0600AD56 RID: 44374 RVA: 0x00291885 File Offset: 0x0028FA85
		public IisTasksValidationInvalidVirtualDirectoryCharException(string virtualDirectory, char badChar, int charIndex, char[] invalidChars, Exception innerException) : base(Strings.IisTasksValidationInvalidVirtualDirectoryCharException(virtualDirectory, badChar, charIndex, invalidChars), innerException)
		{
			this.virtualDirectory = virtualDirectory;
			this.badChar = badChar;
			this.charIndex = charIndex;
			this.invalidChars = invalidChars;
		}

		// Token: 0x0600AD57 RID: 44375 RVA: 0x002918B8 File Offset: 0x0028FAB8
		protected IisTasksValidationInvalidVirtualDirectoryCharException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.virtualDirectory = (string)info.GetValue("virtualDirectory", typeof(string));
			this.badChar = (char)info.GetValue("badChar", typeof(char));
			this.charIndex = (int)info.GetValue("charIndex", typeof(int));
			this.invalidChars = (char[])info.GetValue("invalidChars", typeof(char[]));
		}

		// Token: 0x0600AD58 RID: 44376 RVA: 0x00291950 File Offset: 0x0028FB50
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("virtualDirectory", this.virtualDirectory);
			info.AddValue("badChar", this.badChar);
			info.AddValue("charIndex", this.charIndex);
			info.AddValue("invalidChars", this.invalidChars);
		}

		// Token: 0x170037A6 RID: 14246
		// (get) Token: 0x0600AD59 RID: 44377 RVA: 0x002919A9 File Offset: 0x0028FBA9
		public string VirtualDirectory
		{
			get
			{
				return this.virtualDirectory;
			}
		}

		// Token: 0x170037A7 RID: 14247
		// (get) Token: 0x0600AD5A RID: 44378 RVA: 0x002919B1 File Offset: 0x0028FBB1
		public char BadChar
		{
			get
			{
				return this.badChar;
			}
		}

		// Token: 0x170037A8 RID: 14248
		// (get) Token: 0x0600AD5B RID: 44379 RVA: 0x002919B9 File Offset: 0x0028FBB9
		public int CharIndex
		{
			get
			{
				return this.charIndex;
			}
		}

		// Token: 0x170037A9 RID: 14249
		// (get) Token: 0x0600AD5C RID: 44380 RVA: 0x002919C1 File Offset: 0x0028FBC1
		public char[] InvalidChars
		{
			get
			{
				return this.invalidChars;
			}
		}

		// Token: 0x0400610C RID: 24844
		private readonly string virtualDirectory;

		// Token: 0x0400610D RID: 24845
		private readonly char badChar;

		// Token: 0x0400610E RID: 24846
		private readonly int charIndex;

		// Token: 0x0400610F RID: 24847
		private readonly char[] invalidChars;
	}
}
