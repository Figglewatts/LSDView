﻿#version 330 core

flat in vec4 vertexColor;

out vec4 out_FragColor;

void main()
{
	out_FragColor = vertexColor;
}