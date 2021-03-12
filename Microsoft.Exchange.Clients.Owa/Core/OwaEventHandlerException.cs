using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public sealed class OwaEventHandlerException : OwaTransientException
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0005E5F3 File Offset: 0x0005C7F3
		public OwaEventHandlerException() : base(null)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0005E607 File Offset: 0x0005C807
		public OwaEventHandlerException(string message, string description, OwaEventHandlerErrorCode errorCode, bool hideDebugInformation, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
			this.description = description;
			this.errorCode = errorCode;
			this.hideDebugInformation = hideDebugInformation;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0005E635 File Offset: 0x0005C835
		public OwaEventHandlerException(string message, string description, OwaEventHandlerErrorCode errorCode, Exception innerException, object thisObject) : this(message, description, errorCode, false, innerException, thisObject)
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0005E645 File Offset: 0x0005C845
		public OwaEventHandlerException(string message, string description, Exception innerException) : this(message, description, OwaEventHandlerErrorCode.NotSet, innerException, null)
		{
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0005E653 File Offset: 0x0005C853
		public OwaEventHandlerException(string message, string description) : this(message, description, OwaEventHandlerErrorCode.NotSet, null, null)
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0005E661 File Offset: 0x0005C861
		public OwaEventHandlerException(string message, string description, OwaEventHandlerErrorCode errorCode, bool hideDebugInformation) : this(message, description, errorCode, hideDebugInformation, null, null)
		{
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0005E670 File Offset: 0x0005C870
		public OwaEventHandlerException(string message, string description, bool hideDebugInformation) : this(message, description, OwaEventHandlerErrorCode.NotSet, hideDebugInformation, null, null)
		{
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0005E67F File Offset: 0x0005C87F
		public OwaEventHandlerException(string message, string description, OwaEventHandlerErrorCode errorCode) : this(message, description, errorCode, null, null)
		{
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0005E68C File Offset: 0x0005C88C
		public OwaEventHandlerException(string message) : this(message, message, OwaEventHandlerErrorCode.NotSet, null, null)
		{
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0005E69A File Offset: 0x0005C89A
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0005E6A2 File Offset: 0x0005C8A2
		public bool HideDebugInformation
		{
			get
			{
				return this.hideDebugInformation;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0005E6AA File Offset: 0x0005C8AA
		public new OwaEventHandlerErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04000A20 RID: 2592
		private string description = string.Empty;

		// Token: 0x04000A21 RID: 2593
		private OwaEventHandlerErrorCode errorCode;

		// Token: 0x04000A22 RID: 2594
		private bool hideDebugInformation;
	}
}
