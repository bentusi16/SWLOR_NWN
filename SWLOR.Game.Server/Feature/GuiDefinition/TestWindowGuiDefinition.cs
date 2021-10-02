﻿using SWLOR.Game.Server.Feature.GuiDefinition.ViewModel;
using SWLOR.Game.Server.Service.GuiService;

namespace SWLOR.Game.Server.Feature.GuiDefinition
{
    public class TestWindowGuiDefinition:  IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<TestWindowViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.TestWindow)
                .BindOnOpened(model => model.OnWindowOpen())

                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .BindText(model => model.ButtonText)
                            .BindOnClicked(model => model.OnClickedFirstButton());

                        row.AddButtonImage()
                            .SetImageResref("ife_animal");

                        row.AddToggleButton()
                            .SetText("toggle button")
                            .BindIsToggled(model => model.IsToggled);

                        row.AddLabel()
                            .SetText("label");
                    });

                    col.AddRow(row =>
                    {
                        row.AddComboBox()
                            .BindSelectedIndex(model => model.SelectedComboBoxValue)
                            .AddOption("item 1", 1)
                            .AddOption("item 2", 2)
                            .AddOption("item 3", 3);

                        row.AddColorPicker()
                            .BindSelectedColor(model => model.SelectedColor);

                        row.AddCheckBox()
                            .SetText("checkbox")
                            .BindIsChecked(model => model.IsChecked);

                        row.AddImage()
                            .SetResref("ife_defarrow");
                    });

                    col.AddRow(row =>
                    {
                        row.AddOptions()
                            .BindSelectedValue(model => model.SelectedOption)
                            .AddOption("option 1")
                            .AddOption("option 2");

                        row.AddProgressBar()
                            .SetValue(0.4f);

                        row.AddSliderFloat()
                            .SetMaximum(100f)
                            .SetMinimum(0f)
                            .SetValue(30f);

                        row.AddSliderInt()
                            .SetMaximum(50)
                            .SetMinimum(0)
                            .SetValue(15);
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();

                        row.AddText()
                            .BindText(model => model.EnteredText);

                    });

                    col.AddRow(row =>
                    {
                        row.AddTextEdit()
                            .BindPlaceholder(model => model.Placeholder);
                        //.BindValue(model => model.EnteredText);
                    });
                });


            return _builder.Build();
        }
    }
}