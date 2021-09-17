﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Protobuf.Primitives
{
    /// <summary>
    /// String message wrapper.
    /// </summary>
    public class StringMessage : Message<string>
    {
        private static readonly ICodeGenerator<string> _CodeGenerator = MessageBuilder.GetCodeGenerator<string>();

        /// <summary>
        /// Initialize string message wrapper.
        /// </summary>
        public StringMessage() { }

        /// <summary>
        /// Initialize string message wrapper with a value.
        /// </summary>
        /// <param name="value">Value.</param>
        public StringMessage(string value) : base(value) { }

        /// <inheritdoc/>
        protected override int CalculateSize()
        {
            return CodedOutputStream.ComputeStringSize(SourceValue);
        }

        /// <inheritdoc/>
        protected override void Read(ref ParseContext parser)
        {
            SourceValue = parser.ReadString();
        }

        /// <inheritdoc/>
        protected override void Write(ref WriteContext writer)
        {
            writer.WriteString(SourceValue);
        }
    }
}
