﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001EF RID: 495
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SecurityException : SystemException
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x000680A5 File Offset: 0x000662A5
		[SecuritySafeCritical]
		internal static string GetResString(string sResourceName)
		{
			PermissionSet.s_fullTrust.Assert();
			return Environment.GetResourceString(sResourceName);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000680B8 File Offset: 0x000662B8
		[SecurityCritical]
		internal static Exception MakeSecurityException(AssemblyName asmName, Evidence asmEvidence, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			HostProtectionPermission hostProtectionPermission = permThatFailed as HostProtectionPermission;
			if (hostProtectionPermission != null)
			{
				return new HostProtectionException(SecurityException.GetResString("HostProtection_HostProtection"), HostProtectionPermission.protectedResources, hostProtectionPermission.Resources);
			}
			string message = "";
			MethodInfo method = null;
			try
			{
				if (granted == null && refused == null && demand == null)
				{
					message = SecurityException.GetResString("Security_NoAPTCA");
				}
				else if (demand != null && demand is IPermission)
				{
					message = string.Format(CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), demand.GetType().AssemblyQualifiedName);
				}
				else if (permThatFailed != null)
				{
					message = string.Format(CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), permThatFailed.GetType().AssemblyQualifiedName);
				}
				else
				{
					message = SecurityException.GetResString("Security_GenericNoType");
				}
				method = SecurityRuntime.GetMethodInfo(rmh);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException)
				{
					throw;
				}
			}
			return new SecurityException(message, asmName, granted, refused, method, action, demand, permThatFailed, asmEvidence);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000681A8 File Offset: 0x000663A8
		private static byte[] ObjectToByteArray(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			byte[] result;
			try
			{
				binaryFormatter.Serialize(memoryStream, obj);
				byte[] array = memoryStream.ToArray();
				result = array;
			}
			catch (NotSupportedException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000681F0 File Offset: 0x000663F0
		private static object ByteArrayToObject(byte[] array)
		{
			if (array == null || array.Length == 0)
			{
				return null;
			}
			MemoryStream serializationStream = new MemoryStream(array);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(serializationStream);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0006821C File Offset: 0x0006641C
		[__DynamicallyInvokable]
		public SecurityException() : base(SecurityException.GetResString("Arg_SecurityException"))
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00068239 File Offset: 0x00066439
		[__DynamicallyInvokable]
		public SecurityException(string message) : base(message)
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0006824D File Offset: 0x0006644D
		[SecuritySafeCritical]
		public SecurityException(string message, Type type) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.m_typeOfPermissionThatFailed = type;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00068272 File Offset: 0x00066472
		[SecuritySafeCritical]
		public SecurityException(string message, Type type, string state) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.m_typeOfPermissionThatFailed = type;
			this.m_demanded = state;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0006829E File Offset: 0x0006649E
		[__DynamicallyInvokable]
		public SecurityException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000682B4 File Offset: 0x000664B4
		[SecurityCritical]
		internal SecurityException(PermissionSet grantedSetObj, PermissionSet refusedSetObj) : base(SecurityException.GetResString("Arg_SecurityException"))
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			if (grantedSetObj != null)
			{
				this.m_granted = grantedSetObj.ToXml().ToString();
			}
			if (refusedSetObj != null)
			{
				this.m_refused = refusedSetObj.ToXml().ToString();
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00068310 File Offset: 0x00066510
		[SecurityCritical]
		internal SecurityException(string message, PermissionSet grantedSetObj, PermissionSet refusedSetObj) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			if (grantedSetObj != null)
			{
				this.m_granted = grantedSetObj.ToXml().ToString();
			}
			if (refusedSetObj != null)
			{
				this.m_refused = refusedSetObj.ToXml().ToString();
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00068364 File Offset: 0x00066564
		[SecuritySafeCritical]
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			try
			{
				this.m_action = (SecurityAction)info.GetValue("Action", typeof(SecurityAction));
				this.m_permissionThatFailed = (string)info.GetValueNoThrow("FirstPermissionThatFailed", typeof(string));
				this.m_demanded = (string)info.GetValueNoThrow("Demanded", typeof(string));
				this.m_granted = (string)info.GetValueNoThrow("GrantedSet", typeof(string));
				this.m_refused = (string)info.GetValueNoThrow("RefusedSet", typeof(string));
				this.m_denied = (string)info.GetValueNoThrow("Denied", typeof(string));
				this.m_permitOnly = (string)info.GetValueNoThrow("PermitOnly", typeof(string));
				this.m_assemblyName = (AssemblyName)info.GetValueNoThrow("Assembly", typeof(AssemblyName));
				this.m_serializedMethodInfo = (byte[])info.GetValueNoThrow("Method", typeof(byte[]));
				this.m_strMethodInfo = (string)info.GetValueNoThrow("Method_String", typeof(string));
				this.m_zone = (SecurityZone)info.GetValue("Zone", typeof(SecurityZone));
				this.m_url = (string)info.GetValueNoThrow("Url", typeof(string));
			}
			catch
			{
				this.m_action = (SecurityAction)0;
				this.m_permissionThatFailed = "";
				this.m_demanded = "";
				this.m_granted = "";
				this.m_refused = "";
				this.m_denied = "";
				this.m_permitOnly = "";
				this.m_assemblyName = null;
				this.m_serializedMethodInfo = null;
				this.m_strMethodInfo = null;
				this.m_zone = SecurityZone.NoZone;
				this.m_url = "";
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0006859C File Offset: 0x0006679C
		[SecuritySafeCritical]
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.Action = action;
			if (permThatFailed != null)
			{
				this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
			}
			this.FirstPermissionThatFailed = permThatFailed;
			this.Demanded = demanded;
			this.m_granted = ((grant == null) ? "" : grant.ToXml().ToString());
			this.m_refused = ((refused == null) ? "" : refused.ToXml().ToString());
			this.m_denied = "";
			this.m_permitOnly = "";
			this.m_assemblyName = assemblyName;
			this.Method = method;
			this.m_url = "";
			this.m_zone = SecurityZone.NoZone;
			if (evidence != null)
			{
				Url hostEvidence = evidence.GetHostEvidence<Url>();
				if (hostEvidence != null)
				{
					this.m_url = hostEvidence.GetURLString().ToString();
				}
				Zone hostEvidence2 = evidence.GetHostEvidence<Zone>();
				if (hostEvidence2 != null)
				{
					this.m_zone = hostEvidence2.SecurityZone;
				}
			}
			this.m_debugString = this.ToString(true, false);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000686A4 File Offset: 0x000668A4
		[SecuritySafeCritical]
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.Action = SecurityAction.Demand;
			if (permThatFailed != null)
			{
				this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
			}
			this.FirstPermissionThatFailed = permThatFailed;
			this.Demanded = demanded;
			this.m_granted = "";
			this.m_refused = "";
			this.DenySetInstance = deny;
			this.PermitOnlySetInstance = permitOnly;
			this.m_assemblyName = null;
			this.Method = method;
			this.m_zone = SecurityZone.NoZone;
			this.m_url = "";
			this.m_debugString = this.ToString(true, false);
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00068748 File Offset: 0x00066948
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x00068750 File Offset: 0x00066950
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0006875C File Offset: 0x0006695C
		// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x000687A7 File Offset: 0x000669A7
		public Type PermissionType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_typeOfPermissionThatFailed == null)
				{
					object obj = XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
					if (obj == null)
					{
						obj = XMLUtil.XmlStringToSecurityObject(this.m_demanded);
					}
					if (obj != null)
					{
						this.m_typeOfPermissionThatFailed = obj.GetType();
					}
				}
				return this.m_typeOfPermissionThatFailed;
			}
			set
			{
				this.m_typeOfPermissionThatFailed = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x000687B0 File Offset: 0x000669B0
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x000687C2 File Offset: 0x000669C2
		public IPermission FirstPermissionThatFailed
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return (IPermission)XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
			}
			set
			{
				this.m_permissionThatFailed = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000687D0 File Offset: 0x000669D0
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x000687D8 File Offset: 0x000669D8
		public string PermissionState
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_demanded;
			}
			set
			{
				this.m_demanded = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000687E1 File Offset: 0x000669E1
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x000687EE File Offset: 0x000669EE
		[ComVisible(false)]
		public object Demanded
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_demanded);
			}
			set
			{
				this.m_demanded = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x000687FC File Offset: 0x000669FC
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x00068804 File Offset: 0x00066A04
		public string GrantedSet
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_granted;
			}
			set
			{
				this.m_granted = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0006880D File Offset: 0x00066A0D
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x00068815 File Offset: 0x00066A15
		public string RefusedSet
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_refused;
			}
			set
			{
				this.m_refused = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0006881E File Offset: 0x00066A1E
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0006882B File Offset: 0x00066A2B
		[ComVisible(false)]
		public object DenySetInstance
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_denied);
			}
			set
			{
				this.m_denied = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00068839 File Offset: 0x00066A39
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x00068846 File Offset: 0x00066A46
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_permitOnly);
			}
			set
			{
				this.m_permitOnly = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00068854 File Offset: 0x00066A54
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x0006885C File Offset: 0x00066A5C
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_assemblyName;
			}
			set
			{
				this.m_assemblyName = value;
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00068865 File Offset: 0x00066A65
		private MethodInfo getMethod()
		{
			return (MethodInfo)SecurityException.ByteArrayToObject(this.m_serializedMethodInfo);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00068877 File Offset: 0x00066A77
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x00068880 File Offset: 0x00066A80
		[ComVisible(false)]
		public MethodInfo Method
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.getMethod();
			}
			set
			{
				RuntimeMethodInfo runtimeMethodInfo = value as RuntimeMethodInfo;
				this.m_serializedMethodInfo = SecurityException.ObjectToByteArray(runtimeMethodInfo);
				if (runtimeMethodInfo != null)
				{
					this.m_strMethodInfo = runtimeMethodInfo.ToString();
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000688B5 File Offset: 0x00066AB5
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x000688BD File Offset: 0x00066ABD
		public SecurityZone Zone
		{
			get
			{
				return this.m_zone;
			}
			set
			{
				this.m_zone = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000688C6 File Offset: 0x00066AC6
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000688CE File Offset: 0x00066ACE
		public string Url
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000688D8 File Offset: 0x00066AD8
		private void ToStringHelper(StringBuilder sb, string resourceString, object attr)
		{
			if (attr == null)
			{
				return;
			}
			string text = attr as string;
			if (text == null)
			{
				text = attr.ToString();
			}
			if (text.Length == 0)
			{
				return;
			}
			sb.Append(Environment.NewLine);
			sb.Append(SecurityException.GetResString(resourceString));
			sb.Append(Environment.NewLine);
			sb.Append(text);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00068930 File Offset: 0x00066B30
		[SecurityCritical]
		private string ToString(bool includeSensitiveInfo, bool includeBaseInfo)
		{
			PermissionSet.s_fullTrust.Assert();
			StringBuilder stringBuilder = new StringBuilder();
			if (includeBaseInfo)
			{
				stringBuilder.Append(base.ToString());
			}
			if (this.Action > (SecurityAction)0)
			{
				this.ToStringHelper(stringBuilder, "Security_Action", this.Action);
			}
			this.ToStringHelper(stringBuilder, "Security_TypeFirstPermThatFailed", this.PermissionType);
			if (includeSensitiveInfo)
			{
				this.ToStringHelper(stringBuilder, "Security_FirstPermThatFailed", this.m_permissionThatFailed);
				this.ToStringHelper(stringBuilder, "Security_Demanded", this.m_demanded);
				this.ToStringHelper(stringBuilder, "Security_GrantedSet", this.m_granted);
				this.ToStringHelper(stringBuilder, "Security_RefusedSet", this.m_refused);
				this.ToStringHelper(stringBuilder, "Security_Denied", this.m_denied);
				this.ToStringHelper(stringBuilder, "Security_PermitOnly", this.m_permitOnly);
				this.ToStringHelper(stringBuilder, "Security_Assembly", this.m_assemblyName);
				this.ToStringHelper(stringBuilder, "Security_Method", this.m_strMethodInfo);
			}
			if (this.m_zone != SecurityZone.NoZone)
			{
				this.ToStringHelper(stringBuilder, "Security_Zone", this.m_zone);
			}
			if (includeSensitiveInfo)
			{
				this.ToStringHelper(stringBuilder, "Security_Url", this.m_url);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00068A60 File Offset: 0x00066C60
		[SecurityCritical]
		private bool CanAccessSensitiveInfo()
		{
			bool result = false;
			try
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Demand();
				result = true;
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00068A94 File Offset: 0x00066C94
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ToString(this.CanAccessSensitiveInfo(), true);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00068AA4 File Offset: 0x00066CA4
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Action", this.m_action, typeof(SecurityAction));
			info.AddValue("FirstPermissionThatFailed", this.m_permissionThatFailed, typeof(string));
			info.AddValue("Demanded", this.m_demanded, typeof(string));
			info.AddValue("GrantedSet", this.m_granted, typeof(string));
			info.AddValue("RefusedSet", this.m_refused, typeof(string));
			info.AddValue("Denied", this.m_denied, typeof(string));
			info.AddValue("PermitOnly", this.m_permitOnly, typeof(string));
			info.AddValue("Assembly", this.m_assemblyName, typeof(AssemblyName));
			info.AddValue("Method", this.m_serializedMethodInfo, typeof(byte[]));
			info.AddValue("Method_String", this.m_strMethodInfo, typeof(string));
			info.AddValue("Zone", this.m_zone, typeof(SecurityZone));
			info.AddValue("Url", this.m_url, typeof(string));
		}

		// Token: 0x04000A6C RID: 2668
		private string m_debugString;

		// Token: 0x04000A6D RID: 2669
		private SecurityAction m_action;

		// Token: 0x04000A6E RID: 2670
		[NonSerialized]
		private Type m_typeOfPermissionThatFailed;

		// Token: 0x04000A6F RID: 2671
		private string m_permissionThatFailed;

		// Token: 0x04000A70 RID: 2672
		private string m_demanded;

		// Token: 0x04000A71 RID: 2673
		private string m_granted;

		// Token: 0x04000A72 RID: 2674
		private string m_refused;

		// Token: 0x04000A73 RID: 2675
		private string m_denied;

		// Token: 0x04000A74 RID: 2676
		private string m_permitOnly;

		// Token: 0x04000A75 RID: 2677
		private AssemblyName m_assemblyName;

		// Token: 0x04000A76 RID: 2678
		private byte[] m_serializedMethodInfo;

		// Token: 0x04000A77 RID: 2679
		private string m_strMethodInfo;

		// Token: 0x04000A78 RID: 2680
		private SecurityZone m_zone;

		// Token: 0x04000A79 RID: 2681
		private string m_url;

		// Token: 0x04000A7A RID: 2682
		private const string ActionName = "Action";

		// Token: 0x04000A7B RID: 2683
		private const string FirstPermissionThatFailedName = "FirstPermissionThatFailed";

		// Token: 0x04000A7C RID: 2684
		private const string DemandedName = "Demanded";

		// Token: 0x04000A7D RID: 2685
		private const string GrantedSetName = "GrantedSet";

		// Token: 0x04000A7E RID: 2686
		private const string RefusedSetName = "RefusedSet";

		// Token: 0x04000A7F RID: 2687
		private const string DeniedName = "Denied";

		// Token: 0x04000A80 RID: 2688
		private const string PermitOnlyName = "PermitOnly";

		// Token: 0x04000A81 RID: 2689
		private const string Assembly_Name = "Assembly";

		// Token: 0x04000A82 RID: 2690
		private const string MethodName_Serialized = "Method";

		// Token: 0x04000A83 RID: 2691
		private const string MethodName_String = "Method_String";

		// Token: 0x04000A84 RID: 2692
		private const string ZoneName = "Zone";

		// Token: 0x04000A85 RID: 2693
		private const string UrlName = "Url";
	}
}
