if (!isObject(GuiEditorRolloutProfile)) new GuiControlProfile (GuiEditorRolloutProfile)
{
	// font
    fontType = "Arial";
    fontSize = "14";
	fontColor = "255 253 253 255";
	fontColorHL = "146 145 145 255";
   
	justify = "Center";
	opaque = true;
	border = "0";
  
	bitmap = "^EditorToy/assets/gui/images/rollout_dark.png";
   
	textOffset = "0 -1";
   fillColor = "37 37 37 255";
   fillColorHL = "52 52 53 255";
   borderColor = "12 12 12 255";
   fontColors[0] = "255 253 253 255";
   fontColors[1] = "146 145 145 255";
   borderThickness = "0";

};

if (!isObject(GuiEditorTextProfile)) new GuiControlProfile (GuiEditorTextProfile)
{
    border=false;

    // font
    fontType = "Arial";
    fontSize = "14";

    fontColor = "white";

    modal = true;
    justify = "left";
    autoSizeWidth = false;
    autoSizeHeight = false;
    returnTab = false;
    numbersOnly = false;
    cursorColor = "0 0 0 255";
};

if (!isObject(GuiEditorTextRightProfile)) new GuiControlProfile (GuiEditorTextRightProfile)
{
    border=false;

    // font
    fontType = "Arial";
    fontSize = "14";

    fontColor = "white";

    modal = true;
    justify = "right";
    autoSizeWidth = false;
    autoSizeHeight = false;
    returnTab = false;
    numbersOnly = false;
    cursorColor = "0 0 0 255";
};

if (!isObject(GuiEditorTextEditProfile)) new GuiControlProfile (GuiEditorTextEditProfile)
{
    // font
    fontType = "Arial";
    fontSize = "14";
    opaque = true;
    justify = "left";
    fillColor = "232 240 248 255";
    fillColorHL = "251 170 0 255";
    fillColorNA = "127 127 127 255";
	//NumbersOnly false so decimal points can be typed in
    numbersOnly = false;
    border = -2;
    bitmap = "^EditorToy/assets/gui/images/textEditFrame";
    borderColor = "40 40 40 10";
    fontColor = "27 59 95 255";
    fontColorHL = "232 240 248 255";
    fontColorNA = "0 0 0 52";
    fontColorSEL = "0 0 0 255";
    textOffset = "4 2";
    autoSizeWidth = false;
    autoSizeHeight = false;
    tab = true;
    canKeyFocus = true;
    returnTab = true;
};

if (!isObject(GuiEditorTextEditProfile2)) new GuiControlProfile (GuiEditorTextEditProfile2)
{
    // font
    fontType = "Arial";
    fontSize = "14";
    opaque = true;
    justify = "left";
    fillColor = "232 240 248 255";
    fillColorHL = "251 170 0 255";
    fillColorNA = "127 127 127 255";
	//NumbersOnly false so decimal points can be typed in
    numbersOnly = false;
    border = -2;
    bitmap = "^EditorToy/assets/gui/images/textEditFrame";
    borderColor = "40 40 40 10";
    fontColor = "27 59 95 255";
    fontColorHL = "232 240 248 255";
    fontColorNA = "0 0 0 52";
    fontColorSEL = "0 0 0 255";
    textOffset = "4 2";
    autoSizeWidth = false;
    autoSizeHeight = false;
    tab = true;
    canKeyFocus = true;
    returnTab = true;
};

if(!isObject(GuiEditorWindowProfile)) new GuiControlProfile (GuiEditorWindowProfile)
{
   opaque = true;
   modal = true;
   justify = "center";
   textOffset = "10 4";
   bitmap = "^EditorToy/assets/gui/images/window.png";
   fillColor = "37 36 35 255";
   fontColor = "white";
};

if(!isObject(GuiEditorScrollProfile)) new GuiControlProfile( GuiEditorScrollProfile )
{
   opaque = true;
   fillColor = "37 36 35 255";
   fillColorHL = "251 170 0 255";
   fillColorNA = "127 127 127 255";
   borderColor = "0 0 0 255";
   border = true;
   bitmap = "^EditorToy/assets/gui/images/scrollBar.png";
   hasBitmapArray = true;
   category = "Tools";
};

if(!isObject(GuiDefaultBorderProfile)) new GuiControlProfile (GuiDefaultBorderProfile)
{
	opaque = true;
	fillColor = "37 36 35 255";
	fillColorHL = "251 170 0 255";
	fillColorNA = "127 127 127 255";
	borderColor = "10 10 10 255 ";
	border = true;
};

if(!isObject(GuiEditorDefaultNonModalProfile)) new GuiControlProfile (GuiEditorDefaultNonModalProfile)
{
	modal = false;
};

if (!isObject(BlueButtonProfile)) new GuiControlProfile (BlueButtonProfile : GuiButtonProfile)
{
    // font
    fontType = "Arial";
    fontSize = "14";
    fontColor = "255 255 255 255";
    fontColorHL = "255 255 255 255";
    bitmap = "^EditorToy/assets/gui/images/blueButton.png";
};

if (!isObject(GuiEditorTextEditNumProfile)) new GuiControlProfile(GuiEditorTextEditNumProfile : GuiEditorTextEditProfile2)
{
   numbersOnly = "1";
};

if(!isObject(PaintCursor)) new GuiCursor(PaintCursor)
{
	hotspost = "1 1";
	renderOffset = "0.0 0.0";
	bitmapName = "^EditorToy/assets/gui/images/PaintMouse";
};

if(!isObject(EraserCursor)) new GuiCursor(EraserCursor)
{
	hotspost = "1 1";
	renderOffset = "0.0 0.0";
	bitmapName = "^EditorToy/assets/gui/images/EraserMouse";
};

