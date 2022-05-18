<template>
<v-layout aligm-start>
    <v-flex>
        <v-data-table :headers="Tabla_Encabezados" :items="categorias" sort-by="calories" class="elevation-1" :search="buscar">
            <template v-slot:top>
                <v-toolbar flat>
                    <v-toolbar-title>Historial de Denuncias</v-toolbar-title>
                    <v-divider class="mx-4" inset vertical></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="buscar" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                    <v-dialog v-model="dialog" max-width="500px">
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                                Agregar
                            </v-btn>
                        </template>
                        <v-card>
                            <v-card-title>
                                <span class="text-h5">{{ formTitle }}</span>
                            </v-card-title>

                            <v-card-text>
                                <v-container>
                                    <v-row>
                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="nombre" label="Nombre"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="apellido" label="Apellido"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="cargo" label="Cargo que desempeña"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="entidad" label="Dependencia"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="fecha" label="Fecha Denuncia"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="hora" label="Hora"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="6">
                                            <v-text-field v-model="descripcion" label="Descripción de lo sucedido"></v-text-field>
                                        </v-col>

                                        <v-col cols="12" sm="6" md="4" v-show="valida">
                                            <div class="red--text" v-for="v in ValidarMensaje" :key="v" v-text="v"></div>
                                        </v-col>
                                    </v-row>
                                </v-container>
                            </v-card-text>

                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="blue darken-1" text @click="CerrarForm">
                                    Cerrar
                                </v-btn>
                                <v-btn color="blue darken-1" text @click="GuardarRegistro">
                                    Guardar
                                </v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>

                    <v-dialog v-model="adModal" max-width="350">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Estás seguro de eliminar el registro?</v-card-title>
                   

                            <v-card-text class="font-weight-bold">
                                ¡ Estás a punto de
                                <span style="color:#F44336;" v-if="adAccion==1">Eliminar</span>                       
                                a: {{ adNombre }} {{adApellido}} con número de
                                Denuncia: {{adidregistro}} !
                            </v-card-text>

                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="green darken-1" text @click="activarDesactivarCerrar">Cancelar</v-btn>
                                <v-btn v-if="adAccion==1" color="orange darken-4" text @click="eliminar">Aceptar</v-btn>                         
                            </v-card-actions>

                        </v-card>
                    </v-dialog>

                </v-toolbar>
            </template>

            <template v-slot:item="props">
                <tr>
                    <td>{{ props.item.iddenuncia }}</td>
                    <td>{{ props.item.nombre }}</td>
                    <td>{{ props.item.apellido }}</td>
                    <td>{{ props.item.cargo }}</td>
                    <td>{{ props.item.entidad }}</td>
                    <td>{{ props.item.fecha }}</td>
                    <td>{{ props.item.hora }}</td>
                    <td>{{ props.item.descripcion }}</td>
                    <td>
                        <v-icon small class="mr-2" @click="EditarItem(props.item)">edit</v-icon>

                        <template v-if="props.item.opcion">
                            <v-icon small @click="activarDesactivarMostrar(2, props.item)">lock_open</v-icon>
                        </template>

                        <template v-else>
                            <v-icon small @click="activarDesactivarMostrar(1, props.item)">delete</v-icon>
                        </template>

                    </td>
                </tr>
            </template>

            <template v-slot:no-data>
                <v-btn color="primary" @click="listar">Actualizar </v-btn>
            </template>
        </v-data-table>
    </v-flex>
</v-layout>
</template>

<script>
import axios from "axios";
export default {
    data: () => ({
        dialog: false,
        dialogDelete: false,
        categorias: [],
        Tabla_Encabezados: [{
                text: "Id Denuncia",
                value: "iddenuncia"
            },
            {
                text: "Nombre del Denunciado",
                value: "nombre"
            },
            {
                text: "Apellidos",
                value: "apellido"
            },
            {
                text: "Cargo de la Persona",
                value: "cargo"
            },
            {
                text: "Dependencia",
                value: "entidad"
            },
            {
                text: "Fecha Denuncia",
                value: "fecha"
            },
            {
                text: "Hora",
                value: "hora"
            },
            {
                text: "Indicador de Currupción",
                value: "descripcion"
            },
            {
                text: "Opciones",
                value: "opcion"
            }
        ],
        buscar: '',
        editedIndex: -1,
        id: "",
        nombre: "",
        apellido: "",
        cargo: "",
        entidad: "",
        fecha: "",
        hora: "",
        descripcion: "",
        valida: 0,
        ValidarMensaje: [],
        adModal: 0,
        adAccion: 0,
        adNombre: "",
        adApellido: "",
        adidregistro: "",
        adId: "",
    }),

    computed: {
        formTitle() {
            return this.editedIndex === -1 ? "Nueva Denuncia" : "Editar Denuncia";
        },
    },

    created() {
        this.listar();
    },

    methods: {
        listar() {
            let me = this;
            axios
                .get("api/Categorias/Listar")
                .then(function (response) {
                    //console.log(response);
                    me.categorias = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        EditarItem(item) {
            this.id = item.iddenuncia;
            this.nombre = item.nombre;
            this.apellido = item.apellido;
            this.cargo = item.cargo;
            this.entidad = item.entidad;
            this.fecha = item.fecha;
            this.hora = item.hora;
            this.descripcion = item.descripcion;
            this.editedIndex = 1;
            this.dialog = true;
        },
        CerrarForm() {
            this.dialog = false;
            this.LimpiarForm();
        },
        LimpiarForm() {
            this.iddenuncia = "";
            this.nombre = "";
            this.apellido = "";
            this.cargo = "";
            this.entidad = "";
            this.fecha = "";
            this.hora = "";
            this.descripcion = "";
            this.editedIndex = -1;
        },
        GuardarRegistro() {
            if (this.ValidarForm()) {
                return;
            }
            if (this.editedIndex > -1) {
                //Codigo para editar
                let guardar = this;
                axios
                    .put("api/Categorias/Actualizar", {
                        'iddenuncia': guardar.id,
                        'nombre': guardar.nombre,
                        'apellido': guardar.apellido,
                        'cargo': guardar.cargo,
                        'entidad': guardar.entidad,
                        'fecha': guardar.fecha,
                        'hora': guardar.hora,
                        'descripcion': guardar.descripcion

                    })
                    .then(function (response) {
                        guardar.LimpiarForm();
                        guardar.listar();
                        guardar.CerrarForm();
                    })
                    .catch(function (error) {
                        console.log(error);
                    });

            } else {
                //codigo guaradr
                let guardar = this;
                axios
                    .post("api/Categorias/Crear", {
                        'nombre': guardar.nombre,
                        'apellido': guardar.apellido,
                        'cargo': guardar.cargo,
                        'entidad': guardar.entidad,
                        'fecha': guardar.fecha,
                        'hora': guardar.hora,
                        'descripcion': guardar.descripcion

                    })
                    .then(function (response) {
                        guardar.LimpiarForm();
                        guardar.listar();
                        guardar.CerrarForm();
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },
        ValidarForm() {
            this.valida = 0;
            this.ValidarMensaje = [];
            if (this.nombre.length < 3 || this.nombre.length > 50) {
                this.ValidarMensaje.push(
                    "El nombre debe tener mas de 3 carácteres y menos de 50 carácteres"
                );
            }
            if (this.ValidarMensaje.length) {
                this.valida = 1;
            }
            return this.valida;
        },
        activarDesactivarMostrar(accion, item) {
            this.adModal = 1;
            this.adNombre = item.nombre;
            this.adApellido = item.apellido;
            this.adidregistro = item.iddenuncia;
            this.adId = item.iddenuncia;
            if (accion == 1) {
                this.adAccion = 1;
            } else if (accion == 2) {
                this.adAccion = 2;
            } else {
                this.adModal = 0;

            }
        },
        activarDesactivarCerrar() {
            this.adModal = 0;
        },
        eliminar() {
            let guardar = this;
            axios
                .delete("api/Categorias/Eliminar/" + this.adId, {})
                .then(function (response) {
                    //console.log(response);
                    guardar.adModal = 0;
                    guardar.adAccion = 0;
                    guardar.listar();
                })
                .catch(function (error) {
                    console.log(error);
                });
        },
    },
};
</script>
