// Global using directives

global using AutoMapper;
global using Boilerplate.Api.Middlewares;
global using Boilerplate.Api.Security.Authorization;
global using Boilerplate.Infrastructure;
global using Boilerplate.Infrastructure.Repository;
global using Boilerplate.Domain.Configurations;
global using Boilerplate.Domain.DTOs;
global using Boilerplate.Domain.Exceptions;
global using Boilerplate.Application.Interfaces;
global using Boilerplate.Application.Services;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Security.Claims;
global using System.Text.Encodings.Web;
global using System.Threading.Tasks;
global using Boilerplate.Api.DTOs.Requests.Dummy;
global using Boilerplate.Api.DTOs.Responses.Dummy;
global using Boilerplate.Api.Security.Authentication;
global using Microsoft.OpenApi.Models;
global using IConfigurationProvider = AutoMapper.IConfigurationProvider;
