using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		// Token: 0x06002013 RID: 8211 RVA: 0x00070E86 File Offset: 0x0006F086
		public PrivilegeNotHeldException() : base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
		{
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00070E98 File Offset: 0x0006F098
		public PrivilegeNotHeldException(string privilege) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege))
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00070EBC File Offset: 0x0006F0BC
		public PrivilegeNotHeldException(string privilege, Exception inner) : base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00070EE1 File Offset: 0x0006F0E1
		internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00070EFC File Offset: 0x0006F0FC
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00070F04 File Offset: 0x0006F104
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x04000B8F RID: 2959
		private readonly string _privilegeName;
	}
}
