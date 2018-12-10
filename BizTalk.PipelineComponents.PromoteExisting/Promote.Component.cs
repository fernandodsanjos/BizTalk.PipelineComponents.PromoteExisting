using System;
using System.Collections;
using System.Linq;
using BizTalkComponents.Utils;
using System.ComponentModel;

namespace BizTalk.PipelineComponents.PromoteExisting
{
    public partial class Promote
    {
        public string Name { get { return "PromoteExisting"; } }
        public string Version { get { return "1.0"; } }
        public string Description { get { return "Promotes Existing Context"; } }

        [Browsable(false)]
        public void GetClassID(out Guid classID)
        {
            classID = new Guid("bc4ef586-bc79-4be9-b1d7-cda74953a468");
        }
        [Browsable(false)]
        public void InitNew()
        {

        }
        [Browsable(false)]
        public IEnumerator Validate(object projectSystem)
        {
            return ValidationHelper.Validate(this, false).ToArray().GetEnumerator();
        }
        [Browsable(false)]
        public bool Validate(out string errorMessage)
        {
            var errors = ValidationHelper.Validate(this, true).ToArray();

            if (errors.Any())
            {
                errorMessage = string.Join(",", errors);

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }

        [Browsable(false)]
        public IntPtr Icon { get { return IntPtr.Zero; } }
    }
}
